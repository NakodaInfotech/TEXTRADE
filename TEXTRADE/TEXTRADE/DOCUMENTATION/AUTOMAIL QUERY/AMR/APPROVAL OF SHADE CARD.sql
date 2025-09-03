DECLARE @SUBJECT VARCHAR(100), @PARTYEMAIL VARCHAR(100)
DECLARE @DETAILSBODY VARCHAR(MAX),   @ITEMNAME VARCHAR(100), @ITEMCODE VARCHAR(100)
SET @SUBJECT = 'Approval For Shade Card ' 


declare CUR_MAINNAME cursor for  SELECT ITEMMASTER.item_code AS ITEMCODE , ITEMMASTER.item_name AS ITEMNAME ,   LEDGERS.Acc_email AS PARTYEMAIL
FROM            ITEMMASTER LEFT OUTER JOIN LEDGERS ON ITEMMASTER.ITEM_LEDGERID = LEDGERS.Acc_id
WHERE ISNULL(LEDGERS.Acc_email,'') <> '' AND ITEM_SHADEAPPDATE ='/  /' -- AND TRY_CAST(ITEMMASTER.ITEM_SHADESENDDATE AS date ) = CAST(GETDATE() - 1 AS DATE)
ORDER BY LEDGERS.Acc_cmpname
open CUR_MAINNAME
	fetch next from CUR_MAINNAME into @ITEMCODE, @ITEMNAME, @PARTYEMAIL
	while @@Fetch_STATUS = 0
		begin
			--FOR DETAILS***********************************************************
			begin
			
			SET @DETAILSBODY = ''
			declare CUR_MAIN cursor for SELECT  ITEMMASTER.item_code AS ITEMCODE ,  ITEMMASTER.item_name AS ITEMNAME 
FROM            ITEMMASTER LEFT OUTER JOIN LEDGERS ON ITEMMASTER.ITEM_LEDGERID = LEDGERS.Acc_id
WHERE ISNULL(LEDGERS.Acc_email,'') <> '' AND ITEM_SHADEAPPDATE ='/  /'  --AND TRY_CAST(ITEMMASTER.ITEM_SHADESENDDATE AS date ) = CAST(GETDATE() - 1 AS DATE) 
AND ITEMMASTER.item_name = @ITEMNAME AND ITEMMASTER.item_code = @ITEMCODE
			ORDER BY ITEMMASTER.item_code
			open CUR_MAIN
				fetch next from CUR_MAIN into @ITEMCODE, @ITEMNAME 
				while @@Fetch_STATUS = 0
					begin
			SET @DETAILSBODY = @DETAILSBODY + N'<tr>'
			SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" width="250px">' + CAST(@ITEMCODE as VARCHAR) + N'</TD>'
			SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" width="250px">' + CAST(@ITEMNAME as VARCHAR) + N'</TD>'
			SET @DETAILSBODY = @DETAILSBODY + N'</tr>'

						fetch next from CUR_MAIN into @ITEMCODE, @ITEMNAME
					END
			close CUR_MAIN
			deallocate CUR_MAIN
			end

					
DECLARE @DBODY VARCHAR(MAX)
			SET @DBODY =	N'<H1 style="font-family:Tahoma; font-size:11px;">DETAILS</H1>' +
				N'<Table Border = "1">' + 
				N'<Tr style="font-family:Tahoma; font-size:11px;"><Th>Item Code</Th><Th>Item Name</Th></Tr>'+
				+ISNULL(@DETAILSBODY,'')+ N'<tfoot><tr>
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



		fetch next from CUR_MAINNAME into  @ITEMCODE, @ITEMNAME, @PARTYEMAIL
		END
close CUR_MAINNAME
deallocate CUR_MAINNAME
