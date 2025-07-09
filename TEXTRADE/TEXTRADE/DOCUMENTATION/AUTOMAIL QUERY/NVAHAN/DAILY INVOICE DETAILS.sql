DECLARE @SUBJECT VARCHAR(100), @ENTRYBODY VARCHAR(MAX), @PARTYEMAIL VARCHAR(100), @NAME VARCHAR(100), @PARTYPCS INT, @PARTYMTRS DECIMAL(18,2)
SET @SUBJECT = 'INVOICE DETAILS REPORT FOR ' +  CAST(DAY(GETDATE()-3) AS VARCHAR(2)) + '-' + CAST(MONTH(GETDATE()-3) AS VARCHAR(2)) + '-' + CAST(YEAR(GETDATE()-3) AS VARCHAR(4))


declare CUR_MAINNAME cursor for SELECT DISTINCT LEDGERS.Acc_cmpname AS NAME, LEDGERS.Acc_email
FROM            INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id 
WHERE cast(INVOICEMASTER.INVOICE_DATE as date) = CAST(GETDATE()-3 AS DATE) AND ISNULL(LEDGERS.Acc_email,'') <> ''
ORDER BY LEDGERS.Acc_cmpname
open CUR_MAINNAME
	fetch next from CUR_MAINNAME into  @NAME, @PARTYEMAIL
	while @@Fetch_STATUS = 0
		begin


			SELECT @PARTYPCS = SUM(INVOICEMASTER.INVOICE_TOTALPCS), @PARTYMTRS = SUM(INVOICEMASTER.INVOICE_TOTALMTRS)
			FROM   INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id 
			WHERE cast(INVOICEMASTER.INVOICE_DATE as date)  = CAST(GETDATE()-3 AS DATE) AND LEDGERS.Acc_cmpname = @NAME



			--FOR DETAILS***********************************************************
			begin
			DECLARE @DETAILSBODY VARCHAR(MAX),  @INVNO VARCHAR(100), @ITEMNAME VARCHAR(100), @BALENO VARCHAR(100), @LRNO VARCHAR(100), @MTRS DECIMAL(18,2), @PCS INT, @RATE DECIMAL(18,2)
			SET @DETAILSBODY = ''
			declare CUR_MAIN cursor for SELECT INVOICEMASTER.INVOICE_PRINTINITIALS AS INVNO, ITEMMASTER.item_name AS ITEMNAME, ISNULL(INVOICEMASTER_DESC.INVOICE_BALENO, '') AS BALENO, 
									 ISNULL(INVOICEMASTER.INVOICE_LRNO, '') AS LRNO, INVOICEMASTER_DESC.INVOICE_PCS AS PCS, INVOICEMASTER_DESC.INVOICE_MTRS AS MTRS, INVOICEMASTER_DESC.INVOICE_RATE AS RATE
			FROM            INVOICEMASTER INNER JOIN
									 INVOICEMASTER_DESC ON INVOICEMASTER.INVOICE_NO = INVOICEMASTER_DESC.INVOICE_NO AND INVOICEMASTER.INVOICE_REGISTERID = INVOICEMASTER_DESC.INVOICE_REGISTERID AND 
									 INVOICEMASTER.INVOICE_YEARID = INVOICEMASTER_DESC.INVOICE_YEARID INNER JOIN
									 LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id INNER JOIN
									 ITEMMASTER ON INVOICEMASTER_DESC.INVOICE_ITEMID = ITEMMASTER.item_id
			WHERE cast(INVOICEMASTER.INVOICE_DATE as date)  = CAST(GETDATE()-3 AS DATE) AND LEDGERS.Acc_cmpname = @NAME
			ORDER BY INVOICEMASTER.INVOICE_NO, INVOICE_SRNO
			open CUR_MAIN
				fetch next from CUR_MAIN into  @INVNO, @ITEMNAME, @BALENO, @LRNO, @PCS, @MTRS, @RATE
				while @@Fetch_STATUS = 0
					begin

						SET @DETAILSBODY = @DETAILSBODY + N'<tr>'
						SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" width="150px">' + @INVNO + N'</TD>'
						SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" width="250px">' + @ITEMNAME + N'</TD>'
						SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" width="80px">' + @BALENO + N'</TD>'
						SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" width="100px">' + @LRNO + N'</TD>'
						SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" align="right" width="60px">' + CAST(@PCS as VARCHAR) + N'</TD>'
						SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" align="right" width="60px">' + CAST(@MTRS as VARCHAR) + N'</TD>'
						SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" align="right" width="60px">' + CAST(@RATE as VARCHAR) + N'</TD>'
						SET @DETAILSBODY = @DETAILSBODY + N'</tr>'

						fetch next from CUR_MAIN into  @INVNO, @ITEMNAME, @BALENO, @LRNO, @PCS, @MTRS, @RATE
					END
			close CUR_MAIN
			deallocate CUR_MAIN
			end

			DECLARE @DBODY VARCHAR(MAX)
			SET @DBODY = N'<H1 style="font-family:Tahoma; font-size:11px;">' + @NAME + ' - INVOICE DETAILS</H1>' +
							N'<Table Border = "1">' + 
							N'<Tr style="font-family:Tahoma; font-size:11px;"><Th>Invoice No</Th><Th>Item Name</Th><Th>Bale No</Th><Th>LR No</Th><Th>Pcs</Th><Th>Mtrs</Th><Th>Rate</Th></Tr>'+
							+ISNULL(@DETAILSBODY,'')+ N'<tfoot><tr><td colspan="4"; style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" bgcolor="yellow">GRAND TOTAL:</td>
							<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="yellow">' + CAST(@PARTYPCS AS VARCHAR) + N'</td>
							<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="yellow">' + CAST(@PARTYMTRS AS VARCHAR) + N'</td>
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
