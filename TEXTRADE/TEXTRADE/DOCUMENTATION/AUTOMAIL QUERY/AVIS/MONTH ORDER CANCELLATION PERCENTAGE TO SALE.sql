DECLARE @SUBJECT VARCHAR(100), @ENTRYBODY VARCHAR(MAX), @TOTALTAXABLE DECIMAL(18,2), @TOTALMTRS DECIMAL(18,2), @TOTALCANCELAMT DECIMAL(18,2), @TOTALCANCELMTRS DECIMAL(18,2), @PERCENTAGE DECIMAL(18,2), @CMPNAME AS VARCHAR(100), @YEARID INT
SET @CMPNAME = 'SUPRIYA SILK INDUSTRIES'
SET @YEARID=(SELECT TOP 1 YEAR_ID FROM YEARMASTER inner join CMPMASTER ON YEAR_CMPID = CMP_ID WHERE CMP_DISPLAYEDNAME = @CMPNAME  and year_startdate= CASE WHEN MONTH(GETDATE()) > 3 THEN CAST(YEAR(GETDATE()) AS VARCHAR(4)) + '-04-01' ELSE CAST(YEAR(GETDATE())-1 AS VARCHAR(4)) + '-04-01' END ORDER BY YEAR_STARTDATE DESC)

SET @SUBJECT = 'SSI-MONTHLY ORDER CANCELLATION WITH PERCENTAGE TO SALE REPORT FOR ' + UPPER(DATENAME(MONTH,  DATEADD(MONTH,-1, GETDATE())  ))

set @TOTALTAXABLE = 0
set @TOTALCANCELAMT=  0
SET @TOTALMTRS = 0
SET @TOTALCANCELMTRS = 0
SET @PERCENTAGE = 0

begin
DECLARE @DETAILSBODY VARCHAR(MAX),  @NAME VARCHAR(100), @TAXABLEAMT DECIMAL(18,2), @MTRS DECIMAL(18,2), @CANCELLEDAMT DECIMAL(18,2), @CANCELLEDMTRS DECIMAL(18,2)
SET @DETAILSBODY = ''
declare CUR_MAIN cursor for SELECT NAME, SUM(TAXABLEAMT) AS TAXABLEAMT, SUM(TOTALMTRS) AS TOTALMTRS, SUM(CANCELLEDAMT) AS CANCELLEDAMT, SUM(CANCELLEDMTRS) AS CANCELLEDMTRS FROM
							(
							SELECT LEDGERS.Acc_cmpname AS NAME, SUM(INVOICE_SUBTOTAL) AS TAXABLEAMT, SUM(INVOICE_TOTALMTRS) AS TOTALMTRS, 0 AS CANCELLEDAMT, 0 AS CANCELLEDMTRS 
							FROM INVOICEMASTER INNER JOIN LEDGERS ON INVOICE_LEDGERID = ACC_ID 
							WHERE MONTH(cast(INVOICEMASTER.INVOICE_DATE as date))  = MONTH(DATEADD(MONTH,-1, GETDATE())) and INVOICEMASTER.INVOICE_YEARID = @YEARID
							GROUP BY LEDGERS.Acc_cmpname
							UNION ALL
							SELECT LEDGERS.Acc_cmpname AS NAME, 0 AS TAXABLEAMT, 0 AS MTRS, SUM(SO_AMOUNT) AS CANCELLEDAMT, SUM(SO_MTRS) AS CANCELMTRS 
							FROM SALEORDER_DESC INNER JOIN SALEORDER ON SALEORDER_DESC.SO_NO = SALEORDER.SO_NO AND SALEORDER_DESC.SO_YEARID = SALEORDER.SO_YEARID INNER JOIN LEDGERS ON so_ledgerid = ACC_ID 
							WHERE SO_CLOSED = 0 AND MONTH(cast(SALEORDER.SO_DATE as date))  = MONTH(DATEADD(MONTH,-1, GETDATE())) AND SALEORDER.SO_YEARID = @YEARID
							GROUP BY LEDGERS.Acc_cmpname
							) AS T
							GROUP BY NAME 
							ORDER BY T.NAME

open CUR_MAIN
	fetch next from CUR_MAIN into  @NAME, @TAXABLEAMT, @MTRS, @CANCELLEDAMT, @CANCELLEDMTRS
	while @@Fetch_STATUS = 0
		begin
			
			SET @TOTALTAXABLE = @TOTALTAXABLE + @TAXABLEAMT
			SET @TOTALCANCELAMT = @TOTALCANCELAMT + @CANCELLEDAMT
			SET @TOTALMTRS = @TOTALMTRS + @MTRS
			SET @TOTALCANCELMTRS = @TOTALCANCELMTRS + @CANCELLEDMTRS
			SET @PERCENTAGE = 0

			IF @CANCELLEDMTRS > 0 AND @MTRS > 0
			BEGIN
				SET @PERCENTAGE = ROUND((@CANCELLEDMTRS/@MTRS)*100,2)
			END


			SET @DETAILSBODY = @DETAILSBODY + N'<tr>'
			SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" width="250px">' + @NAME + N'</TD>'
			SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" align="right" width="100px">' + CAST(@TAXABLEAMT as VARCHAR) + N'</TD>'
			SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" align="right" width="100px">' + CAST(@CANCELLEDAMT as VARCHAR) + N'</TD>'
			SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" align="right" width="100px">' + CAST(@MTRS as VARCHAR) + N'</TD>'
			SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" align="right" width="100px">' + CAST(@CANCELLEDMTRS as VARCHAR) + N'</TD>'
			SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" align="right" width="60px">' + CAST(@PERCENTAGE as VARCHAR) + N'</TD>'
			SET @DETAILSBODY = @DETAILSBODY + N'</tr>'

			fetch next from CUR_MAIN into  @NAME, @TAXABLEAMT, @MTRS, @CANCELLEDAMT, @CANCELLEDMTRS
		END
close CUR_MAIN
deallocate CUR_MAIN
end

DECLARE @DBODY VARCHAR(MAX)
SET @DBODY = N'<H1 style="font-family:Tahoma; font-size:11px;">DETAILS</H1>' +
				N'<Table Border = "1">' + 
				N'<Tr style="font-family:Tahoma; font-size:11px;"><Th>Buyer Name</Th><Th>Taxable Amt</Th><Th>Cancelled Amt</Th><Th>Sale Mtrs</Th><Th>Cancelled Mtrs</Th><Th>%</Th></Tr>'+
				+ISNULL(@DETAILSBODY,'')+ N'<tfoot><tr><td colspan="1"; style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" bgcolor="yellow">GRAND TOTAL:</td>
				<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="yellow">' + CAST(@TOTALTAXABLE AS VARCHAR) + N'</td>
				<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="yellow">' + CAST(@TOTALCANCELAMT AS VARCHAR) + N'</td>
				<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="yellow">' + CAST(@TOTALMTRS AS VARCHAR) + N'</td>
				<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="yellow">' + CAST(@TOTALCANCELMTRS AS VARCHAR) + N'</td>
				</tr></tfoot></Table></html>'

--FOR DETAILS***********************************************************


DECLARE @FINALBODY VARCHAR(MAX)
SET @FINALBODY = @DBODY


EXEC msdb.dbo.sp_send_dbmail
@profile_name='TEXTRADE',
@recipients='gulkitjain@gmail.com;',
@subject=@SUBJECT,
@body=@FINALBODY,
@body_format = 'HTML'
