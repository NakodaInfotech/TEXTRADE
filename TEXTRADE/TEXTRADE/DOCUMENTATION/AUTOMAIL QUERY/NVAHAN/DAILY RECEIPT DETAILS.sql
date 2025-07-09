DECLARE @SUBJECT VARCHAR(100), @ENTRYBODY VARCHAR(MAX), @PARTYEMAIL VARCHAR(100), @NAME VARCHAR(100), @TOTALRECAMT INT, @PARTYMTRS DECIMAL(18,2)
SET @SUBJECT = 'RECEIPT DETAILS REPORT FOR ' +  CAST(DAY(GETDATE()-1) AS VARCHAR(2)) + '-' + CAST(MONTH(GETDATE()-1) AS VARCHAR(2)) + '-' + CAST(YEAR(GETDATE()-1) AS VARCHAR(4))


declare CUR_MAINNAME cursor for SELECT DISTINCT LEDGERS.Acc_cmpname AS NAME, LEDGERS.Acc_email
FROM            RECEIPTMASTER INNER JOIN LEDGERS ON RECEIPTMASTER.RECEIPT_LEDGERID = LEDGERS.Acc_id 
WHERE cast(RECEIPTMASTER.RECEIPT_DATE as date) = CAST(GETDATE()-1 AS DATE) AND ISNULL(LEDGERS.Acc_email,'') <> ''
ORDER BY LEDGERS.Acc_cmpname
open CUR_MAINNAME
	fetch next from CUR_MAINNAME into  @NAME, @PARTYEMAIL
	while @@Fetch_STATUS = 0
		begin


			SELECT @TOTALRECAMT = SUM(RECEIPT_REPORT.RECEIPTAMT)
			FROM   RECEIPT_REPORT 
			WHERE cast(RECEIPTDATE as date)  = CAST(GETDATE()-1 AS DATE) AND NAME = @NAME



			--FOR DETAILS***********************************************************
			begin
			DECLARE @DETAILSBODY VARCHAR(MAX), @PARTYBANKNAME VARCHAR(100), @CHQNO VARCHAR(100), @BILLNO VARCHAR(100), @BILLDATE DATE, @BILLAMT DECIMAL(18,2), @EXTRAAMT DECIMAL(18,2), @PAYTYPE VARCHAR(20), @RECAMT DECIMAL(18,2)
			SET @DETAILSBODY = ''
			declare CUR_MAIN cursor for SELECT PARTYBANKNAME, CHQNO, BILLINITIALS, isnull(BILLDATE, getdate()), BILLAMT, EXTRAAMT, PAYTYPE, RECEIPTAMT 
			from RECEIPT_REPORT 
			WHERE CAST(RECEIPT_REPORT.RECEIPTDATE AS DATE) = CAST(GETDATE()-1 AS DATE) AND NAME = @NAME
			ORDER BY RECEIPTNO, RECEIPT_REPORT.BILLNO
			open CUR_MAIN
				fetch next from CUR_MAIN into @PARTYBANKNAME, @CHQNO, @BILLNO, @BILLDATE, @BILLAMT, @EXTRAAMT, @PAYTYPE, @RECAMT
				while @@Fetch_STATUS = 0
					begin

						SET @DETAILSBODY = @DETAILSBODY + N'<tr>'
						SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" width="150px">' + @PARTYBANKNAME + N'</TD>'
						SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" width="150px">' + @CHQNO + N'</TD>'
						SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" width="100px">' + @BILLNO + N'</TD>'
						SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" width="80px">' + CAST(@BILLDATE AS VARCHAR) + N'</TD>'
						SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" align="right" width="80px">' + CAST(@BILLAMT AS VARCHAR) + N'</TD>'
						SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" align="right" width="80px">' + CAST(@EXTRAAMT AS VARCHAR) + N'</TD>'
						SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" width="100px">' + @PAYTYPE + N'</TD>'
						SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" align="right" width="80px">' + CAST(@RECAMT as VARCHAR) + N'</TD>'
						SET @DETAILSBODY = @DETAILSBODY + N'</tr>'

						fetch next from CUR_MAIN into @PARTYBANKNAME, @CHQNO, @BILLNO, @BILLDATE, @BILLAMT, @EXTRAAMT, @PAYTYPE, @RECAMT
					END
			close CUR_MAIN
			deallocate CUR_MAIN
			end

			DECLARE @DBODY VARCHAR(MAX)
			SET @DBODY = N'<H1 style="font-family:Tahoma; font-size:11px;">' + @NAME + ' - RECEIPT DETAILS</H1>' +
							N'<Table Border = "1">' + 
							N'<Tr style="font-family:Tahoma; font-size:11px;"><Th>Bank Name</Th><Th>Chq/Neft/RTGS No</Th><Th>Bill No</Th><Th>Bill Date</Th><Th>Bill Amt</Th><Th>Extra Amt</Th><Th>Pay Type</Th><Th>Rec Amt</Th></Tr>'+
							+ISNULL(@DETAILSBODY,'')+ N'<tfoot><tr><td colspan="7"; style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" bgcolor="yellow">GRAND TOTAL:</td>
							<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="yellow">' + CAST(@TOTALRECAMT AS VARCHAR) + N'</td>
							</tr></tfoot></Table></html>'

			--FOR DETAILS***********************************************************


			DECLARE @FINALBODY VARCHAR(MAX)
			SET @FINALBODY = @DBODY


			EXEC msdb.dbo.sp_send_dbmail
			@profile_name='TEXTRADE',
			@recipients = @PARTYEMAIL,
			@subject=@SUBJECT,
			@body=@FINALBODY,
			@body_format = 'HTML'



		fetch next from CUR_MAINNAME into   @NAME, @PARTYEMAIL
		END
close CUR_MAINNAME
deallocate CUR_MAINNAME
