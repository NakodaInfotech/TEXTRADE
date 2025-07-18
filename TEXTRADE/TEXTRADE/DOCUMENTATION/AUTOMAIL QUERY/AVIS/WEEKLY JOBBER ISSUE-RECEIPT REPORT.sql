DECLARE @SUBJECT VARCHAR(100), @ENTRYBODY VARCHAR(MAX), @TOTALRECEIPT DECIMAL(18,2), @TOTALISSUE DECIMAL(18,2), @CMPNAME VARCHAR(100), @YEARID INT, @BODY VARCHAR(MAX)
SET @CMPNAME = 'SUPRIYA SILK MILLS PVT. LTD.'
SET @YEARID=(SELECT TOP 1 YEAR_ID FROM YEARMASTER inner join CMPMASTER ON YEAR_CMPID = CMP_ID WHERE CMP_DISPLAYEDNAME = @CMPNAME  and year_startdate= CASE WHEN MONTH(GETDATE()) > 3 THEN CAST(YEAR(GETDATE()) AS VARCHAR(4)) + '-04-01' ELSE CAST(YEAR(GETDATE())-1 AS VARCHAR(4)) + '-04-01' END ORDER BY YEAR_STARTDATE DESC)

SET @SUBJECT = 'SSMPL-WEEKLY JOBBER ISSUE-RECEIPT REPORT LAST 2 WEEKS'


--FIRST WEEK
SET @TOTALISSUE = 0
SET @TOTALRECEIPT = 0

SELECT @TOTALISSUE = ISNULL(SUM(GRN.GRN_TOTALMTRS),0) FROM GRN WHERE GRN.GRN_YEARID = @YEARID AND CAST(GRN_DATE AS DATE) > CAST(DATEADD(day, -14, GETDATE()) AS DATE) AND CAST(GRN_DATE AS DATE) < CAST(DATEADD(day, -7, GETDATE()) AS DATE) AND ISNULL(GRN_TOLEDGERID,0) <> 0 AND GRN_TYPE = 'Job Work'
SELECT @TOTALISSUE = @TOTALISSUE + ISNULL(SUM(JOBOUT.JO_TOTALMTRS),0) FROM JOBOUT WHERE JOBOUT.JO_YEARID = @YEARID AND CAST(JO_DATE AS DATE) > CAST(DATEADD(day, -14, GETDATE()) AS DATE) AND CAST(JO_DATE AS DATE) < CAST(DATEADD(day, -7, GETDATE()) AS DATE)

SELECT @TOTALRECEIPT =  SUM(MATREC_TOTALRECDMTR) FROM MATERIALRECEIPT WHERE MATERIALRECEIPT.MATREC_YEARID = @YEARID  AND CAST(MATREC_DATE AS DATE) > CAST(DATEADD(day, -14, GETDATE()) AS DATE) AND CAST(MATREC_DATE AS DATE) < CAST(DATEADD(day, -7, GETDATE()) AS DATE)
SELECT @TOTALRECEIPT = @TOTALRECEIPT + ISNULL(SUM(JOBIN.JI_TOTALMTRS),0) FROM JOBIN WHERE JOBIN.JI_YEARID = @YEARID AND CAST(JI_DATE AS DATE) > CAST(DATEADD(day, -14, GETDATE()) AS DATE) AND CAST(JI_DATE AS DATE) < CAST(DATEADD(day, -7, GETDATE()) AS DATE)


SET @ENTRYBODY = (SELECT
			[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@width]='250px', TD= T.JOBBERNAME,'', 
			[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@align]='right',[TD/@width]='100px', TD=CAST(SUM(T.ISSUEMTRS)AS DECIMAL(18,2)) ,'',
			[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@align]='right',[TD/@width]='100px', TD=CAST(SUM(T.RECMTRS)AS DECIMAL(18,2)),''
			FROM
			(
			SELECT LEDGERS.Acc_cmpname AS JOBBERNAME, SUM(GRN_TOTALMTRS) AS ISSUEMTRS, 0 AS RECMTRS FROM GRN INNER JOIN LEDGERS ON GRN_TOLEDGERID = LEDGERS.Acc_id WHERE GRN.GRN_YEARID = @YEARID AND CAST(GRN_DATE AS DATE) > CAST(DATEADD(day, -14, GETDATE()) AS DATE) AND CAST(GRN_DATE AS DATE) < CAST(DATEADD(day, -7, GETDATE()) AS DATE) AND ISNULL(GRN_TOLEDGERID,0) <> 0 AND GRN_TYPE = 'Job Work' GROUP BY LEDGERS.Acc_cmpname
			UNION ALL
			SELECT LEDGERS.Acc_cmpname AS JOBBERNAME, SUM(JO_TOTALMTRS) AS ISSUEMTRS, 0 AS RECMTRS FROM JOBOUT INNER JOIN LEDGERS ON JO_LEDGERID = LEDGERS.Acc_id WHERE JOBOUT.JO_YEARID = @YEARID AND CAST(JO_DATE AS DATE) > CAST(DATEADD(day, -14, GETDATE()) AS DATE) AND CAST(JO_DATE AS DATE) < CAST(DATEADD(day, -7, GETDATE()) AS DATE) GROUP BY LEDGERS.Acc_cmpname
			UNION ALL
			SELECT LEDGERS.Acc_cmpname AS JOBBERNAME, 0 AS ISSUEMTRS, SUM(MATREC_TOTALRECDMTR) AS RECMTRS FROM MATERIALRECEIPT INNER JOIN LEDGERS ON MATREC_LEDGERID = LEDGERS.Acc_id WHERE MATERIALRECEIPT.MATREC_YEARID = @YEARID  AND CAST(MATREC_DATE AS DATE) > CAST(DATEADD(day, -14, GETDATE()) AS DATE) AND CAST(MATREC_DATE AS DATE) < CAST(DATEADD(day, -7, GETDATE()) AS DATE) GROUP BY LEDGERS.Acc_cmpname
			UNION ALL
			SELECT LEDGERS.Acc_cmpname AS JOBBERNAME, 0 AS ISSUEMTRS, SUM(JI_TOTALMTRS) AS RECMTRS FROM JOBIN INNER JOIN LEDGERS ON JI_LEDGERID = LEDGERS.Acc_id WHERE JOBIN.JI_YEARID = @YEARID AND CAST(JI_DATE AS DATE) > CAST(DATEADD(day, -14, GETDATE()) AS DATE) AND CAST(JI_DATE AS DATE) < CAST(DATEADD(day, -7, GETDATE()) AS DATE) GROUP BY LEDGERS.Acc_cmpname
			) AS T
			GROUP BY JOBBERNAME
			
			FOR XML PATH ('tr'))
			
SET @BODY = ISNULL(@BODY,'') + N'<H1 style="font-family:Tahoma; font-size:11px;">JOBBER WISE ISSUE-RECEIPT SUMMARY FROM ' + CAST(DAY(DATEADD(day, -14, GETDATE())) AS VARCHAR(2)) +'-' + CAST(MONTH(DATEADD(day, -14, GETDATE())) AS VARCHAR(2)) + '-' + CAST(YEAR(DATEADD(day, -14, GETDATE())) AS VARCHAR(4)) + ' TO ' +  CAST(DAY(DATEADD(day, -8, GETDATE())) AS VARCHAR(2)) +'-' + CAST(MONTH(DATEADD(day, -8, GETDATE())) AS VARCHAR(2)) + '-' + CAST(YEAR(DATEADD(day, -8, GETDATE())) AS VARCHAR(4)) + '</H1>' +
							N'<Table Border = "1">' + 
							N'<Tr style="font-family:Tahoma; font-size:11px;"><Th>Name</Th><Th>Issue Mtrs</Th><Th>Receipt Mtrs</Th></Tr>'+
							+ISNULL(@ENTRYBODY,'')+ N'<tfoot><tr><td colspan="1"; style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" bgcolor="yellow">TOTAL:</td>
							<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="yellow">' + CAST(@TOTALISSUE AS VARCHAR) + N'</td>
							<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="yellow">' + CAST(@TOTALRECEIPT AS VARCHAR) + N'</td>
							</tr></tfoot></Table></html>'



--LAST WEEK
SET @TOTALISSUE = 0
SET @TOTALRECEIPT = 0

SELECT @TOTALISSUE = ISNULL(SUM(GRN.GRN_TOTALMTRS),0) FROM GRN WHERE GRN.GRN_YEARID = @YEARID AND CAST(GRN_DATE AS DATE) > CAST(DATEADD(day, -7, GETDATE()) AS DATE) AND CAST(GRN_DATE AS DATE) < CAST(DATEADD(day, -1, GETDATE()) AS DATE) AND ISNULL(GRN_TOLEDGERID,0) <> 0 AND GRN_TYPE = 'Job Work'
SELECT @TOTALISSUE = @TOTALISSUE + ISNULL(SUM(JOBOUT.JO_TOTALMTRS),0) FROM JOBOUT WHERE JOBOUT.JO_YEARID = @YEARID AND CAST(JO_DATE AS DATE) > CAST(DATEADD(day, -7, GETDATE()) AS DATE) AND CAST(JO_DATE AS DATE) < CAST(DATEADD(day, -1, GETDATE()) AS DATE)

SELECT @TOTALRECEIPT =  SUM(MATREC_TOTALRECDMTR) FROM MATERIALRECEIPT WHERE MATERIALRECEIPT.MATREC_YEARID = @YEARID  AND CAST(MATREC_DATE AS DATE) > CAST(DATEADD(day, -7, GETDATE()) AS DATE) AND CAST(MATREC_DATE AS DATE) < CAST(DATEADD(day, -1, GETDATE()) AS DATE)
SELECT @TOTALRECEIPT = @TOTALRECEIPT + ISNULL(SUM(JOBIN.JI_TOTALMTRS),0) FROM JOBIN WHERE JOBIN.JI_YEARID = @YEARID AND CAST(JI_DATE AS DATE) > CAST(DATEADD(day, -7, GETDATE()) AS DATE) AND CAST(JI_DATE AS DATE) < CAST(DATEADD(day, -1, GETDATE()) AS DATE)



SET @ENTRYBODY = (SELECT
			[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@width]='250px', TD= T.JOBBERNAME,'', 
			[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@align]='right',[TD/@width]='100px', TD=CAST(SUM(T.ISSUEMTRS)AS DECIMAL(18,2)) ,'',
			[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@align]='right',[TD/@width]='100px', TD=CAST(SUM(T.RECMTRS)AS DECIMAL(18,2)),''
			FROM
			(
			SELECT LEDGERS.Acc_cmpname AS JOBBERNAME, SUM(GRN_TOTALMTRS) AS ISSUEMTRS, 0 AS RECMTRS FROM GRN INNER JOIN LEDGERS ON GRN_TOLEDGERID = LEDGERS.Acc_id WHERE GRN.GRN_YEARID = @YEARID AND CAST(GRN_DATE AS DATE) > CAST(DATEADD(day, -7, GETDATE()) AS DATE) AND CAST(GRN_DATE AS DATE) < CAST(GETDATE() AS DATE) AND ISNULL(GRN_TOLEDGERID,0) <> 0 AND GRN_TYPE = 'Job Work' GROUP BY LEDGERS.Acc_cmpname
			UNION ALL
			SELECT LEDGERS.Acc_cmpname AS JOBBERNAME, SUM(JO_TOTALMTRS) AS ISSUEMTRS, 0 AS RECMTRS FROM JOBOUT INNER JOIN LEDGERS ON JO_LEDGERID = LEDGERS.Acc_id WHERE JOBOUT.JO_YEARID = @YEARID AND CAST(JO_DATE AS DATE) > CAST(DATEADD(day, -7, GETDATE()) AS DATE) AND CAST(JO_DATE AS DATE) < CAST(GETDATE() AS DATE) GROUP BY LEDGERS.Acc_cmpname
			UNION ALL
			SELECT LEDGERS.Acc_cmpname AS JOBBERNAME, 0 AS ISSUEMTRS, SUM(MATREC_TOTALRECDMTR) AS RECMTRS FROM MATERIALRECEIPT INNER JOIN LEDGERS ON MATREC_LEDGERID = LEDGERS.Acc_id WHERE MATERIALRECEIPT.MATREC_YEARID = @YEARID  AND CAST(MATREC_DATE AS DATE) > CAST(DATEADD(day, -7, GETDATE()) AS DATE) AND CAST(MATREC_DATE AS DATE) < CAST(GETDATE() AS DATE) GROUP BY LEDGERS.Acc_cmpname
			UNION ALL
			SELECT LEDGERS.Acc_cmpname AS JOBBERNAME, 0 AS ISSUEMTRS, SUM(JI_TOTALMTRS) AS RECMTRS FROM JOBIN INNER JOIN LEDGERS ON JI_LEDGERID = LEDGERS.Acc_id WHERE JOBIN.JI_YEARID = @YEARID AND CAST(JI_DATE AS DATE) > CAST(DATEADD(day, -7, GETDATE()) AS DATE) AND CAST(JI_DATE AS DATE) < CAST(GETDATE() AS DATE) GROUP BY LEDGERS.Acc_cmpname
			) AS T
			GROUP BY JOBBERNAME
			
			FOR XML PATH ('tr'))

SET @BODY = ISNULL(@BODY,'') + N'<H1 style="font-family:Tahoma; font-size:11px;">JOBBER WISE ISSUE-RECEIPT SUMMARY FROM ' + CAST(DAY(DATEADD(day, -7, GETDATE())) AS VARCHAR(2)) +'-' + CAST(MONTH(DATEADD(day, -7, GETDATE())) AS VARCHAR(2)) + '-' + CAST(YEAR(DATEADD(day, -7, GETDATE())) AS VARCHAR(4)) + ' TO ' +  CAST(DAY(DATEADD(day, -1, GETDATE())) AS VARCHAR(2)) +'-' + CAST(MONTH(DATEADD(day, -1, GETDATE())) AS VARCHAR(2)) + '-' + CAST(YEAR(DATEADD(day, -1, GETDATE())) AS VARCHAR(4)) + '</H1>' +
							N'<Table Border = "1">' + 
							N'<Tr style="font-family:Tahoma; font-size:11px;"><Th>Name</Th><Th>Issue Mtrs</Th><Th>Receipt Mtrs</Th></Tr>'+
							+ISNULL(@ENTRYBODY,'')+ N'<tfoot><tr><td colspan="1"; style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" bgcolor="yellow">TOTAL:</td>
							<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="yellow">' + CAST(@TOTALISSUE AS VARCHAR) + N'</td>
							<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="yellow">' + CAST(@TOTALRECEIPT AS VARCHAR) + N'</td>
							</tr></tfoot></Table></html>'

			

DECLARE @FINALBODY VARCHAR(MAX)
SET @FINALBODY = @BODY 


EXEC msdb.dbo.sp_send_dbmail
@profile_name='TEXTRADE',
@recipients='gulkitjain@gmail.com;',
@subject=@SUBJECT,
@body=@FINALBODY,
@body_format = 'HTML'








