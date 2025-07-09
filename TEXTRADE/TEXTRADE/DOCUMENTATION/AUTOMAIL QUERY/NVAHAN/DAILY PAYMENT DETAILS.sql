DECLARE @SUBJECT VARCHAR(100), @ENTRYBODY VARCHAR(MAX), @PARTYEMAIL VARCHAR(100), @NAME VARCHAR(100), @TOTALRECAMT INT, @PARTYMTRS DECIMAL(18,2)
SET @SUBJECT = 'PAYMENT DETAILS REPORT FOR ' +  CAST(DAY(GETDATE()) AS VARCHAR(2)) + '-' + CAST(MONTH(GETDATE()) AS VARCHAR(2)) + '-' + CAST(YEAR(GETDATE()) AS VARCHAR(4))


declare CUR_MAINNAME cursor for SELECT DISTINCT LEDGERS.Acc_cmpname AS NAME, LEDGERS.Acc_email
FROM            PAYMENTMASTER INNER JOIN LEDGERS ON PAYMENTMASTER.PAYMENT_LEDGERID = LEDGERS.Acc_id 
WHERE cast(PAYMENTMASTER.PAYMENT_DATE as date) = CAST(GETDATE() AS DATE) AND ISNULL(LEDGERS.Acc_email,'') <> ''
ORDER BY LEDGERS.Acc_cmpname
open CUR_MAINNAME
	fetch next from CUR_MAINNAME into  @NAME, @PARTYEMAIL
	while @@Fetch_STATUS = 0
		begin


			SELECT @TOTALRECAMT = SUM(PAYMENTMASTER_DESC.PAYMENT_amt)
			FROM   PAYMENTMASTER INNER JOIN PAYMENTMASTER_DESC ON PAYMENTMASTER.PAYMENT_NO = PAYMENTMASTER_DESC.PAYMENT_NO AND PAYMENTMASTER.PAYMENT_registerid = PAYMENTMASTER_DESC.PAYMENT_registerid AND PAYMENTMASTER.PAYMENT_yearid = PAYMENTMASTER_DESC.PAYMENT_yearid INNER JOIN LEDGERS ON PAYMENT_ledgerid = LEDGERS.ACC_ID
			WHERE cast(PAYMENT_DATE as date)  = CAST(GETDATE() AS DATE) AND Acc_cmpname = @NAME



			--FOR DETAILS***********************************************************
			begin
			DECLARE @DETAILSBODY VARCHAR(MAX), @PARTYBANKNAME VARCHAR(100), @CHQNO VARCHAR(100), @BILLNO VARCHAR(100), @BILLDATE DATE, @BILLAMT DECIMAL(18,2), @DEDUCTIONAMT DECIMAL(18,2), @ADJAMT DECIMAL(18,2), @RETURNAMT DECIMAL(18,2), @EXTRAAMT DECIMAL(18,2), @PAYTYPE VARCHAR(20), @RECAMT DECIMAL(18,2), @CMPNAME VARCHAR(50)
			SET @DETAILSBODY = ''
			declare CUR_MAIN cursor for SELECT CHQNO, ISNULL(PAYMENTBILLDETAILS.PARTYBILLNO,''), isnull(PAYMENTBILLDETAILS.BILLDATE, getdate()) AS BILLDATE, ISNULL(PAYMENTBILLDETAILS.BILLAMT,0), ISNULL(PAYMENTBILLDETAILS.DEDUCTION,0), ISNULL(PAYMENTBILLDETAILS.PAIDAMT-PAYMENT_REPORT.PAYMENTAMT,0) AS ADJAMT, ISNULL(PAYMENTBILLDETAILS.RETURNAMT,0), PAYTYPE, PAYMENTAMT, CMP_NAME AS CMPNAME
			from PAYMENT_REPORT LEFT OUTER JOIN PAYMENTBILLDETAILS ON PAYMENT_REPORT.BILLINITIALS = PAYMENTBILLDETAILS.INITIALS AND PAYMENT_REPORT.NAME = PAYMENTBILLDETAILS.NAME AND PAYMENT_REPORT.YEARID = PAYMENTBILLDETAILS.YEARID INNER JOIN CMPMASTER ON PAYMENT_REPORT.CMPID = CMP_ID
			WHERE CAST(PAYMENTDATE AS DATE) = CAST(GETDATE() AS DATE) AND PAYMENT_REPORT.NAME = @NAME
			ORDER BY PAYMENTNO, PAYMENT_REPORT.BILLNO
			open CUR_MAIN
				fetch next from CUR_MAIN into @CHQNO, @BILLNO, @BILLDATE, @BILLAMT, @DEDUCTIONAMT, @ADJAMT, @RETURNAMT, @PAYTYPE, @RECAMT, @CMPNAME
				while @@Fetch_STATUS = 0
					begin

						SET @DETAILSBODY = @DETAILSBODY + N'<tr>'
						SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" width="150px">' + @CHQNO + N'</TD>'
						SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" width="100px">' + @BILLNO + N'</TD>'
						SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" width="80px">' + CAST(@BILLDATE AS VARCHAR) + N'</TD>'
						SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" align="right" width="80px">' + CAST(@BILLAMT AS VARCHAR) + N'</TD>'
						SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" align="right" width="80px">' + CAST(@DEDUCTIONAMT AS VARCHAR) + N'</TD>'
						SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" align="right" width="80px">' + CAST(@ADJAMT AS VARCHAR) + N'</TD>'
						SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" align="right" width="80px">' + CAST(@RETURNAMT AS VARCHAR) + N'</TD>'
						SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" width="100px">' + @PAYTYPE + N'</TD>'
						SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" align="right" width="80px">' + CAST(@RECAMT as VARCHAR) + N'</TD>'
						SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" width="200px">' + @CMPNAME + N'</TD>'
						SET @DETAILSBODY = @DETAILSBODY + N'</tr>'

						fetch next from CUR_MAIN into @CHQNO, @BILLNO, @BILLDATE, @BILLAMT, @DEDUCTIONAMT, @ADJAMT, @RETURNAMT, @PAYTYPE, @RECAMT, @CMPNAME
					END
			close CUR_MAIN
			deallocate CUR_MAIN
			end

			DECLARE @DBODY VARCHAR(MAX)
			SET @DBODY = N'<H1 style="font-family:Tahoma; font-size:11px;">' + @NAME + ' - PAYMENT DETAILS</H1>' +
							N'<Table Border = "1">' + 
							N'<Tr style="font-family:Tahoma; font-size:11px;"><Th>Chq/Neft/RTGS No</Th><Th>Party Bill No</Th><Th>Bill Date</Th><Th>Bill Amt</Th><Th>Deduction</Th><Th>Adj Amt</Th><Th>Return Amt</Th><Th>Pay Type</Th><Th>Paid Amt</Th><Th>Company Name</Th></Tr>'+
							+ISNULL(@DETAILSBODY,'')+ N'<tfoot><tr><td colspan="8"; style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" bgcolor="yellow">GRAND TOTAL:</td>
							<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="yellow">' + CAST(@TOTALRECAMT AS VARCHAR) + N'</td>
							</tr></tfoot></Table></html>'

			--FOR DETAILS***********************************************************


			DECLARE @FINALBODY VARCHAR(MAX)
			SET @FINALBODY = @DBODY


			EXEC msdb.dbo.sp_send_dbmail
			@profile_name='TEXTRADE',
			@recipients = 'gulkitjain@gmail.com', --@PARTYEMAIL,
			@subject=@SUBJECT,
			@body=@FINALBODY,
			@body_format = 'HTML'



		fetch next from CUR_MAINNAME into   @NAME, @PARTYEMAIL
		END
close CUR_MAINNAME
deallocate CUR_MAINNAME
