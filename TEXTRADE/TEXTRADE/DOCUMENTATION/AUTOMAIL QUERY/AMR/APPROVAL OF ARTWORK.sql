DECLARE @SUBJECT VARCHAR(100), @PARTYEMAIL VARCHAR(100) ,@ACCID INT
DECLARE @DETAILSBODY VARCHAR(MAX),   @ITEMNAME VARCHAR(100), @ITEMCODE VARCHAR(100)
SET @SUBJECT = 'Approval For Art Work '

DECLARE @CMPNAME VARCHAR(100), @YEARID INT
SET @CMPNAME = 'AMR PRINT & PACK'
SET @YEARID=(SELECT TOP 1 YEAR_ID FROM YEARMASTER inner join CMPMASTER ON YEAR_CMPID = CMP_ID WHERE CMP_DISPLAYEDNAME = @CMPNAME  and year_startdate= CASE WHEN MONTH(GETDATE()) > 3 THEN CAST(YEAR(GETDATE()) AS VARCHAR(4)) + '-04-01' ELSE CAST(YEAR(GETDATE())-1 AS VARCHAR(4)) + '-04-01' END ORDER BY YEAR_STARTDATE DESC)


declare CUR_MAINNAME cursor for  SELECT  distinct LEDGERS.Acc_id AS ACCID , LEDGERS.Acc_email AS PARTYEMAIL
FROM            LEDGERS inner join ITEMMASTER  on ITEMMASTER.ITEM_LEDGERID = LEDGERS.Acc_id
WHERE ISNULL(LEDGERS.Acc_email,'') <> '' AND  ITEM_PROOFSEND = 1 and  ITEM_PROOFOK = 0 AND LEDGERS.Acc_yearid = @YEARID 

open CUR_MAINNAME
	fetch next from CUR_MAINNAME into @ACCID ,  @PARTYEMAIL
	while @@Fetch_STATUS = 0
		begin
			--FOR DETAILS***********************************************************
			begin
			
			SET @DETAILSBODY = ''
			declare CUR_MAIN cursor for SELECT  ITEMMASTER.item_code AS ITEMCODE ,  ITEMMASTER.item_name AS ITEMNAME FROM LEDGERS  INNER JOIN  ITEMMASTER  ON ITEMMASTER.ITEM_LEDGERID = LEDGERS.Acc_id
			WHERE ISNULL(LEDGERS.Acc_email,'') <> '' AND  ITEM_PROOFSEND = 1 and  ITEM_PROOFOK = 0 AND  LEDGERS.Acc_id = @ACCID
			
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
			SET @DBODY =	N'<html><body>' +
				N'<H1 style="font-family:Tahoma; font-size:11px;">ITEM DETAILS</H1>' +
				N'<Table Border = "1">' + 
				N'<Tr style="font-family:Tahoma; font-size:11px;"><Th>Item Code</Th><Th>Item Name</Th></Tr>'+
				+ISNULL(@DETAILSBODY,'')+ N'<tfoot><tr>
				</tr></tfoot></Table>'+
				  N'<br><b style="font-family:Tahoma; font-size:11px;">Note :- Kindly Ignore if Approved.</b>' +
				  N'</body></html>'


			--FOR DETAILS***********************************************************


			DECLARE @FINALBODY VARCHAR(MAX)
			SET @FINALBODY = @DBODY


			EXEC msdb.dbo.sp_send_dbmail
			@profile_name='TEXTRADE',
			@recipients = @PARTYEMAIL,
			@subject=@SUBJECT,
			@body=@FINALBODY,
			@body_format = 'HTML'



		fetch next from CUR_MAINNAME into @ACCID,  @PARTYEMAIL
		END
close CUR_MAINNAME
deallocate CUR_MAINNAME
