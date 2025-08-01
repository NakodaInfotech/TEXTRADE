DECLARE @SUBJECT VARCHAR(100), @ENTRYBODY VARCHAR(MAX), @TOTALMTRS DECIMAL(18,2), @TOTALPCS DECIMAL(18,2), @CMPNAME AS VARCHAR(100), @YEARID INT
SELECT @CMPNAME = 'SUPRIYA SILK MILLS PVT. LTD.'
SET @YEARID=(SELECT TOP 1 YEAR_ID FROM YEARMASTER inner join CMPMASTER ON YEAR_CMPID = CMP_ID WHERE CMP_DISPLAYEDNAME = @CMPNAME  and year_startdate= CASE WHEN MONTH(GETDATE()) > 3 THEN CAST(YEAR(GETDATE()) AS VARCHAR(4)) + '-04-01' ELSE CAST(YEAR(GETDATE())-1 AS VARCHAR(4)) + '-04-01' END ORDER BY YEAR_STARTDATE DESC)

SELECT @TOTALMTRS = SUM(GDN_MTRS), @TOTALPCS = SUM(GDN_PCS) FROM GDN INNER JOIN GDN_DESC ON GDN.GDN_NO = GDN_DESC.GDN_NO AND GDN.GDN_YEARID = GDN_DESC.GDN_YEARID INNER JOIN LEDGERS ON GDN.GDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN DESIGNMASTER ON GDN_DESIGNID = DESIGN_ID LEFT OUTER JOIN COLORMASTER ON GDN_COLORID = COLOR_ID WHERE  ROUND(ISNULL(GDN.GDN_OUTMTRS,0),0) = 0 AND GDN.GDN_YEARID = @YEARID
SET @SUBJECT = 'SSMPL-PENDING CHALLAN FOR INVOICE ' + CAST(DAY(GETDATE()) AS VARCHAR(2)) + '-' + CAST(MONTH( GETDATE()) AS VARCHAR(2)) + '-' + CAST(YEAR( GETDATE()) AS VARCHAR(4))
DECLARE @BODY VARCHAR(MAX)
SET @BODY = ''


DECLARE @LEDGERNAME AS VARCHAR(100), @PARTYPCS INT, @PARTYMTRS FLOAT
declare CUR_MAIN cursor for select LEDGERS.Acc_cmpname, SUM(GDN_DESC.GDN_PCS) AS PARTYPCS, SUM(GDN_DESC.GDN_MTRS) AS PARTYMTRS from GDN INNER JOIN LEDGERS ON GDN.GDN_LEDGERID = LEDGERS.ACC_ID INNER JOIN GDN_DESC ON GDN.GDN_NO = GDN_DESC.GDN_NO AND GDN.GDN_YEARID = GDN_DESC.GDN_YEARID WHERE ROUND(ISNULL(GDN.GDN_OUTMTRS,0),0) = 0  AND GDN.GDN_YEARID = @YEARID GROUP BY LEDGERS.Acc_cmpname
open CUR_MAIN
	fetch next from CUR_MAIN into @LEDGERNAME, @PARTYPCS,  @PARTYMTRS
	while @@Fetch_STATUS = 0
		begin
			
			SET @ENTRYBODY = (SELECT  
			[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@width]='80px', TD= GDN.GDN_date ,'', 
			[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@width]='250px', TD= ISNULL(LEDGERS.Acc_cmpname,'') ,'', 
			[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@width]='250px', TD= ISNULL(DESIGN_NO, '') ,'', 
			[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@width]='250px', TD= ISNULL(COLOR_NAME, '') ,'', 
			[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@align]='right',[TD/@width]='80px', TD=SUM(CAST(GDN_DESC.GDN_PCS AS DECIMAL(18,2))) ,'',
			[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@align]='right',[TD/@width]='80px', TD=SUM(CAST(GDN_DESC.GDN_MTRS AS DECIMAL(18,2))) ,''

			FROM GDN INNER JOIN GDN_DESC ON GDN.GDN_NO = GDN_DESC.GDN_NO AND GDN.GDN_YEARID = GDN_DESC.GDN_YEARID INNER JOIN 
			LEDGERS ON GDN.GDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN 
			DESIGNMASTER ON GDN_DESIGNID = DESIGN_ID LEFT OUTER JOIN 
			COLORMASTER ON GDN_COLORID = COLOR_ID 
			WHERE LEDGERS.ACC_CMPNAME = @LEDGERNAME  AND ROUND(ISNULL(GDN.GDN_OUTMTRS,0),0) = 0  AND GDN.GDN_YEARID = @YEARID
			GROUP BY GDN_DATE, ISNULL(DESIGN_NO, ''), ISNULL(COLOR_NAME, ''), ISNULL(LEDGERS.ACC_CMPNAME,'')
			ORDER BY GDN.GDN_DATE
			FOR XML PATH ('tr'))



			SET @BODY = @BODY + N'<H1 style="font-family:Tahoma; font-size:11px;">' + @LEDGERNAME + '</H1>' +
							N'<Table Border = "1">' + 
							N'<Tr style="font-family:Tahoma; font-size:11px;"><Th>Date</Th><Th>Name</Th><Th>Design No</Th><Th>Shade</Th><Th>Pcs</Th><Th>Mtrs</Th></Tr>'+
							+ISNULL(@ENTRYBODY,'')+ N'<tfoot><tr><td colspan="4"; style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" bgcolor="yellow">PARTY TOTAL:</td>
							<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="yellow">' + CAST(@PARTYPCS AS VARCHAR) + N'</td>
							<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="yellow">' + CAST(@PARTYMTRS AS VARCHAR) + N'</td>
							</tr></tfoot></Table></html>'

fetch next from CUR_MAIN into @LEDGERNAME, @PARTYPCS,  @PARTYMTRS
		end
close CUR_MAIN
deallocate CUR_MAIN




SET @BODY = @BODY + N'<Table Border = "1">' + 
							 N'<tfoot><tr><td colspan="2"; style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" bgcolor="lightgreen">GRAND TOTAL:</td>
							<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="lightgreen">' + CAST(@TOTALPCS AS VARCHAR) + N'</td>
							<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="lightgreen">' + CAST(@TOTALMTRS AS VARCHAR) + N'</td>
							</tr></tfoot></Table></html>'




DECLARE @FINALBODY VARCHAR(MAX)
SET @FINALBODY = @BODY 


EXEC msdb.dbo.sp_send_dbmail
@profile_name='TEXTRADE',
@recipients='royaltexfabrics@yahoo.co.in;',
@subject=@SUBJECT,
@body=@FINALBODY,
@body_format = 'HTML'