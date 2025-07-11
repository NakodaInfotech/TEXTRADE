DECLARE @SUBJECT VARCHAR(100), @YEARID INT, @ENTRYBODY VARCHAR(MAX), @TOTALDYEINGMTRS DECIMAL(18,2), @TOTALMTRS DECIMAL(18,2), @TOTALPENDINGCHALLANMTRS DECIMAL(18,2), @CMPNAME AS VARCHAR(100)
SET @CMPNAME = 'SUPRIYA SILK MILLS PVT. LTD.'
SET @YEARID=(SELECT TOP 1 YEAR_ID FROM YEARMASTER inner join CMPMASTER ON YEAR_CMPID = CMP_ID WHERE CMP_DISPLAYEDNAME = @CMPNAME  and year_startdate= CASE WHEN MONTH(GETDATE()) > 3 THEN CAST(YEAR(GETDATE()) AS VARCHAR(4)) + '-04-01' ELSE CAST(YEAR(GETDATE())-1 AS VARCHAR(4)) + '-04-01' END ORDER BY YEAR_STARTDATE DESC)

SET @SUBJECT = 'SSMPL-DAILY STOCK SUMMARY REPORT FOR ' + CAST(DAY( GETDATE()) AS VARCHAR(2)) + '-' + CAST(MONTH( GETDATE()) AS VARCHAR(2)) + '-' + CAST(YEAR( GETDATE()) AS VARCHAR(4))

SELECT @TOTALDYEINGMTRS = SUM(T.BALMTRS) FROM (SELECT  ROUND((CASE WHEN ACCEPTEDMTRS > 0 THEN ROUND(ACCEPTEDMTRS - (SUM(RECDMTRS) +  (CASE WHEN LOTCOMPLETED = 'TRUE' THEN (ACCEPTEDMTRS - SUM(RECDMTRS) - OPRECDMTRS)  ELSE 0 END)) - OPRECDMTRS,2) ELSE TOTALMTRS-OPRECDMTRS END),2) AS BALMTRS FROM LOT_VIEW_DETAILS WHERE YEARID = @YEARID and LOTCOMPLETED='FALSE' GROUP BY PARTYNAME, JOBBERNAME,  LOTNO, QUALITY, DESIGN, ITEMNAME, TOTALPCS, TOTALMTRS, SHORTPCS, SHORTMTRS, CHECKPCS, CHECKMTRS, REJPCS, REJMTRS, ACCEPTEDPCS, ACCEPTEDMTRS, CMPID, YEARID, CHALLANNO, CATEGORYNAME, LOTCOMPLETED, FORPROCESS, PURRATE, LRNO, LOTNODATE, OPRECDMTRS, TRANSNAME) AS T
SELECT @TOTALMTRS = ISNULL(SUM(MTRS),0) FROM BARCODESTOCK WHERE YEARID = @YEARID AND UNIT IN (SELECT UNIT_ABBR FROM DEFAULTSTOCKUNIT)
SELECT @TOTALPENDINGCHALLANMTRS = SUM(T.MTRS) FROM (SELECT ISNULL(SUM(GDN.GDN_TOTALMTRS), 0) AS MTRS FROM GDN WHERE  ROUND(ISNULL(GDN.GDN_OUTPCS,0),0) = 0 AND GDN.GDN_DATE <= CAST(GETDATE() AS DATE)AND GDN.GDN_YEARID = @YEARID UNION ALL SELECT ISNULL(SUM(OPENINGGDN.OPENINGGDN_TOTALMTRS), 0) AS MTRS FROM OPENINGGDN WHERE  ROUND(ISNULL(OPENINGGDN.OPENINGGDN_OUTPCS,0),0) = 0 AND OPENINGGDN.OPENINGGDN_DATE <= CAST(GETDATE() AS DATE) AND OPENINGGDN.OPENINGGDN_YEARID = @YEARID ) AS T


DECLARE @BODY VARCHAR(MAX)
SET @BODY = N'<H1 style="font-family:Tahoma; font-size:11px;">DAILY STOCK SUMMARY</H1>' +
				N'<Table Border = "1">' + 
				N'<Tr style="font-family:Tahoma; font-size:11px;"><Th>.   Dyeing Mtrs   .</Th><Th>.   Stock On Hand   .</Th><Th>.   Pending Challan   .</Th></Tr>'+
				N'<tfoot><tr><td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="yellow">' + CAST(@TOTALDYEINGMTRS AS VARCHAR) + N'</td>
				<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="yellow">' + CAST(@TOTALMTRS AS VARCHAR) + N'</td>
				<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="yellow">' + CAST(@TOTALPENDINGCHALLANMTRS AS VARCHAR) + N'</td>
				</tr></tfoot></Table></html>'



DECLARE @FINALBODY VARCHAR(MAX)
SET @FINALBODY = @BODY 


EXEC msdb.dbo.sp_send_dbmail
@profile_name='TEXTRADE',
@recipients='gulkitjain@gmail.com;',
@subject=@SUBJECT,
@body=@FINALBODY,
@body_format = 'HTML'