DECLARE @SUBJECT VARCHAR(100), @ENTRYBODY VARCHAR(MAX), @TOTALAMT DECIMAL(18,2) , @PENTRYBODY VARCHAR(MAX), @PTOTALAMT DECIMAL(18,2), @CMPNAME AS VARCHAR(100), @YEARID INT
SET @CMPNAME = 'SUPRIYA SILK MILLS PVT. LTD.'
SET @YEARID=(SELECT TOP 1 YEAR_ID FROM YEARMASTER inner join CMPMASTER ON YEAR_CMPID = CMP_ID WHERE CMP_DISPLAYEDNAME = @CMPNAME  and year_startdate= CASE WHEN MONTH(GETDATE()) > 3 THEN CAST(YEAR(GETDATE()) AS VARCHAR(4)) + '-04-01' ELSE CAST(YEAR(GETDATE())-1 AS VARCHAR(4)) + '-04-01' END ORDER BY YEAR_STARTDATE DESC)

SET @SUBJECT = 'SSMPL-WEEKLY RECEIPT-PAYMENT REPORT FROM ' + CAST(DAY(DATEADD(day, -7, GETDATE())) AS VARCHAR(2)) + '-' + CAST(MONTH( DATEADD(day, -7, GETDATE())) AS VARCHAR(2)) + '-' + CAST(YEAR(DATEADD(day, -7, GETDATE())) AS VARCHAR(4))
SELECT @TOTALAMT =  SUM(RECEIPTMASTER.receipt_total)  FROM  RECEIPTMASTER WHERE RECEIPT_DATE > DATEADD(day, -7, GETDATE()) AND RECEIPT_DATE < CAST(GETDATE() AS DATE) AND RECEIPTMASTER.receipt_yearid = @YEARID
SELECT @PTOTALAMT =  SUM(PAYMENTMASTER.PAYMENT_total)  FROM  PAYMENTMASTER WHERE PAYMENT_DATE > DATEADD(day, -7, GETDATE()) AND PAYMENT_DATE < CAST(GETDATE() AS DATE) AND PAYMENTMASTER.PAYMENT_yearid = @YEARID


SET @ENTRYBODY = (SELECT  
[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@width]='80px', TD= RECEIPT_DATE ,'', 
[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@width]='250px', TD= ISNULL(LEDGERS.ACC_CMPNAME,'') ,'', 
[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@align]='right',[TD/@width]='80px', TD=SUM(CAST(RECEIPTMASTER.receipt_total AS DECIMAL(18,2))) ,''

FROM RECEIPTMASTER INNER JOIN LEDGERS ON receipt_ledgerid = LEDGERS.ACC_ID 
WHERE RECEIPT_DATE > DATEADD(day, -7, GETDATE()) AND RECEIPT_DATE < CAST(GETDATE() AS DATE) AND RECEIPTMASTER.receipt_yearid = @YEARID
GROUP BY RECEIPT_DATE, ISNULL(LEDGERS.ACC_CMPNAME,'')
ORDER BY RECEIPT_DATE, ISNULL(LEDGERS.ACC_CMPNAME,'')

FOR XML PATH ('tr'))


DECLARE @BODY VARCHAR(MAX)
SET @BODY = N'<H1 style="font-family:Tahoma; font-size:11px;">WEEKLY RECEIPT DETAILS</H1>' +
				N'<Table Border = "1">' + 
				N'<Tr style="font-family:Tahoma; font-size:11px;"><Th>Date</Th><Th>Name</Th><Th>Amount</Th></Tr>'+
				+ISNULL(@ENTRYBODY,'')+ N'<tfoot><tr><td colspan="2"; style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" bgcolor="yellow">GRAND TOTAL:</td>
				<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="yellow">' + CAST(@TOTALAMT AS VARCHAR) + N'</td>
				</tr></tfoot></Table></html>'



--FOR PAYMENT *****************
SET @PENTRYBODY = (SELECT  
[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@width]='80px', TD= PAYMENT_DATE ,'', 
[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@width]='250px', TD= ISNULL(LEDGERS.ACC_CMPNAME,'') ,'', 
[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@align]='right',[TD/@width]='80px', TD=SUM(CAST(PAYMENTMASTER.PAYMENT_total AS DECIMAL(18,2))) ,''

FROM PAYMENTMASTER INNER JOIN LEDGERS ON PAYMENT_ledgerid = LEDGERS.ACC_ID 
WHERE PAYMENT_DATE > DATEADD(day, -7, GETDATE()) AND PAYMENT_DATE < CAST(GETDATE() AS DATE) AND PAYMENTMASTER.PAYMENT_yearid = @YEARID
GROUP BY PAYMENT_DATE, ISNULL(LEDGERS.ACC_CMPNAME,'')
ORDER BY PAYMENT_DATE, ISNULL(LEDGERS.ACC_CMPNAME,'')

FOR XML PATH ('tr'))


DECLARE @PBODY VARCHAR(MAX)
SET @PBODY = N'<H1 style="font-family:Tahoma; font-size:11px;">WEEKLY PAYMENT DETAILS</H1>' +
				N'<Table Border = "1">' + 
				N'<Tr style="font-family:Tahoma; font-size:11px;"><Th>Date</Th><Th>Name</Th><Th>Amount</Th></Tr>'+
				+ISNULL(@PENTRYBODY,'')+ N'<tfoot><tr><td colspan="2"; style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" bgcolor="yellow">GRAND TOTAL:</td>
				<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="yellow">' + CAST(@PTOTALAMT AS VARCHAR) + N'</td>
				</tr></tfoot></Table></html>'

--FOR PAYMENT *****************




DECLARE @FINALBODY VARCHAR(MAX)
SET @FINALBODY = @BODY + @PBODY


EXEC msdb.dbo.sp_send_dbmail
@profile_name='TEXTRADE',
@recipients='gulkitjain@gmail.com;',
@subject=@SUBJECT,
@body=@FINALBODY,
@body_format = 'HTML'