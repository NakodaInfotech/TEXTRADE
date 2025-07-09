USE TEXTRADE
DECLARE @SUBJECT VARCHAR(100), @ENTRYBODY VARCHAR(MAX), @TOTALPCS INT, @TOTALAMT DECIMAL(18,2)
SELECT @TOTALPCS = SUM(INVOICEMASTER.INVOICE_TOTALPCS)  FROM  INVOICEMASTER WHERE INVOICE_DATE = CAST(GETDATE()-1 AS DATE)
SELECT @TOTALAMT = SUM(INVOICEMASTER.INVOICE_GRANDTOTAL)  FROM  INVOICEMASTER WHERE INVOICE_DATE = CAST(GETDATE()-1 AS DATE)
SET @SUBJECT = 'DAILY INVOICE DETAILS REPORT FOR ' + CAST(DAY( GETDATE()-1) AS VARCHAR(2)) + '-' + CAST(MONTH( GETDATE()-1) AS VARCHAR(2)) + '-' + CAST(YEAR( GETDATE()-1) AS VARCHAR(4))


SET @ENTRYBODY = (SELECT  
[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@width]='80px', TD= T.INVNO ,'', 
[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@width]='80px', TD= T.DATE ,'', 
[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@width]='250px', TD= T.NAME ,'', 
[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@align]='right',[TD/@width]='80px', TD=SUM(CAST(T.PCS AS INT)) ,'',
[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@align]='right',[TD/@width]='100px', TD=CAST(ISNULL(T.GTOTAL,0) AS DECIMAL(18,2)) ,''

FROM 
(
select INVOICEMASTER.INVOICE_NO AS INVNO, INVOICE_date AS DATE, ISNULL(LEDGERS.Acc_cmpname,'') AS NAME, INVOICEMASTER.INVOICE_TOTALPCS AS PCS, INVOICEMASTER.INVOICE_GRANDTOTAL AS GTOTAL
FROM INVOICEMASTER INNER JOIN LEDGERS ON INVOICE_ledgerid = LEDGERS.ACC_ID 
where INVOICE_DATE = CAST(GETDATE()-1 AS DATE)
) AS T
GROUP BY T.INVNO, T.DATE, T.NAME, T.PCS, T.GTOTAL

FOR XML PATH ('tr'))



DECLARE @BODY VARCHAR(MAX)
SET @BODY = N'<H1 style="font-family:Tahoma; font-size:11px;">DAILY INVOICE DETAILS</H1>' +
				N'<Table Border = "1">' + 
				N'<Tr style="font-family:Tahoma; font-size:11px;"><Th>Inv No</Th><Th>Date</Th><Th>Name</Th><Th>Pcs</Th><Th>Grand Total</Th></Tr>'+
				+ISNULL(@ENTRYBODY,'')+ N'<tfoot><tr><td colspan="3"; style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" bgcolor="yellow">GRAND TOTAL:</td>
				<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="yellow">' + CAST(@TOTALPCS AS VARCHAR) + N'</td>
				<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="yellow">' + CAST(@TOTALAMT AS VARCHAR) + N'</td>
				</tr></tfoot></Table></html>'



DECLARE @FINALBODY VARCHAR(MAX)
SET @FINALBODY = @BODY 

	

EXEC msdb.dbo.sp_send_dbmail
@profile_name='TEXTRADE',
@recipients='gulkitjain@gmail.com',	
@subject=@SUBJECT,
@body=@FINALBODY,
@body_format = 'HTML'