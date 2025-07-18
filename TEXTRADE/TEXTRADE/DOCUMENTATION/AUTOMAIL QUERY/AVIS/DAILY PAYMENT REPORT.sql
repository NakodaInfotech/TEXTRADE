DECLARE @SUBJECT VARCHAR(100), @ENTRYBODY VARCHAR(MAX), @TOTALAMT DECIMAL(18,2), @CMPNAME VARCHAR(100), @YEARID INT
SET @CMPNAME = 'SUPRIYA SILK INDUSTRIES'
SET @YEARID=(SELECT TOP 1 YEAR_ID FROM YEARMASTER inner join CMPMASTER ON YEAR_CMPID = CMP_ID WHERE CMP_DISPLAYEDNAME = @CMPNAME  and year_startdate= CASE WHEN MONTH(GETDATE()) > 3 THEN CAST(YEAR(GETDATE()) AS VARCHAR(4)) + '-04-01' ELSE CAST(YEAR(GETDATE())-1 AS VARCHAR(4)) + '-04-01' END ORDER BY YEAR_STARTDATE DESC)

SELECT @TOTALAMT =  SUM(PAYMENTMASTER.PAYMENT_total)  FROM  PAYMENTMASTER WHERE PAYMENT_DATE = CAST(GETDATE() AS DATE) AND PAYMENTMASTER.PAYMENT_YEARID = @YEARID
SET @SUBJECT = 'SSI-DAILY PAYMENT REPORT FOR ' + CAST(DAY( GETDATE()) AS VARCHAR(2)) + '-' + CAST(MONTH( GETDATE()) AS VARCHAR(2)) + '-' + CAST(YEAR( GETDATE()) AS VARCHAR(4))


SET @ENTRYBODY = (SELECT  
[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@width]='80px', TD= PAYMENT_DATE ,'', 
[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@width]='250px', TD= ISNULL(LEDGERS.ACC_CMPNAME,'') ,'', 
[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@align]='right',[TD/@width]='80px', TD=SUM(CAST(PAYMENTMASTER.PAYMENT_total AS DECIMAL(18,2))) ,''

FROM PAYMENTMASTER INNER JOIN LEDGERS ON PAYMENT_ledgerid = LEDGERS.ACC_ID 
WHERE PAYMENT_DATE = CAST(GETDATE() AS DATE) AND PAYMENTMASTER.PAYMENT_YEARID = @YEARID
GROUP BY PAYMENT_DATE, ISNULL(LEDGERS.ACC_CMPNAME,'')




FOR XML PATH ('tr'))


DECLARE @BODY VARCHAR(MAX)
SET @BODY = N'<H1 style="font-family:Tahoma; font-size:11px;">DAILY PAYMENT DETAILS</H1>' +
				N'<Table Border = "1">' + 
				N'<Tr style="font-family:Tahoma; font-size:11px;"><Th>Date</Th><Th>Name</Th><Th>Amount</Th></Tr>'+
				+ISNULL(@ENTRYBODY,'')+ N'<tfoot><tr><td colspan="2"; style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" bgcolor="yellow">GRAND TOTAL:</td>
				<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="yellow">' + CAST(@TOTALAMT AS VARCHAR) + N'</td>
				</tr></tfoot></Table></html>'



DECLARE @FINALBODY VARCHAR(MAX)
SET @FINALBODY = @BODY 


EXEC msdb.dbo.sp_send_dbmail
@profile_name='TEXTRADE',
@recipients='gulkitjain@gmail.com',	--infoavisindustries@gmail.com;gm@avisindustries.in;
@subject=@SUBJECT,
@body=@FINALBODY,
@body_format = 'HTML'