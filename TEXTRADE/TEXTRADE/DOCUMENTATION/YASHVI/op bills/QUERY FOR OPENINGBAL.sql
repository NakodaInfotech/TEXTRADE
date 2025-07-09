
DECLARE @CMPID INT, @YEARID INT, @USERID INT
SET @CMPID = 5
SET @YEARID = 5
SET @USERID = 6

UPDATE ACCOUNTSMASTER SET Acc_opbal = (CASE WHEN TEMPOPENINGBAL.AMOUNT < 0 THEN TEMPOPENINGBAL.AMOUNT * -1 ELSE TEMPOPENINGBAL.AMOUNT END),
Acc_drcr = (CASE WHEN GROUPMASTER.group_secondary = 'Sundry Debtors' then (CASE WHEN TEMPOPENINGBAL.AMOUNT < 0 THEN 'Cr.' ELSE 'Dr.' END) 
else (CASE WHEN TEMPOPENINGBAL.AMOUNT < 0 THEN 'Dr.' ELSE 'Cr.' END) end)
FROM ACCOUNTSMASTER INNER JOIN TEMPOPENINGBAL ON Acc_cmpname = TEMPOPENINGBAL.PARTYNAME
INNER JOIN GROUPMASTER ON group_id = Acc_groupid
WHERE Acc_yearid = @YEARID


--********************************************
--OPEING BALANCES
--********************************************
DELETE FROM ACCMASTER WHERE acc_yearid = @YEARID AND acc_type = 'OPENING'
DECLARE @LEDGERNAME VARCHAR(200), @LEDGERID INT, @OPBAL FLOAT, @DRCR VARCHAR(10)
declare CUR_MAIN cursor for SELECT  ACCOUNTSMASTER.ACC_CMPNAME, ACC_OPBAL , ACC_DRCR
FROM         ACCOUNTSMASTER 
WHERE ACC_YEARID = @YEARID AND ACC_OPBAL > 0
open CUR_MAIN
	fetch next from CUR_MAIN into @LEDGERNAME,@OPBAL, @DRCR
	while @@Fetch_STATUS = 0
		begin
			SET @LEDGERID=0
			SELECT @LEDGERID = dbo.GET_LEDGER_ID (@LEDGERNAME, @CMPID , 0, @YEARID)
			BEGIN
				
				DECLARE @FROMYEAR AS DATE
				SELECT @FROMYEAR = YEARMASTER.year_startdate FROM YEARMASTER WHERE year_id = @YEARID

				if @opbal > 0 
				BEGIN
					DECLARE @ACCID INT, @DATE AS VARCHAR(20)
					SET @DATE = '04/01/' + cast(YEAR(@FROMYEAR) as varchar)
					IF @drcr = 'Dr.'
						BEGIN
							
							select @ACCID = TEXTRADE.dbo.GET_LEDGER_ID('Opening A/C' ,@CMPID, 0,@YEARID)
							--***************** ACCID(CREDIT) AND ACCNAME(DEBIT) **********************
							INSERT INTO ACCMASTER
							VALUES
							(
								@ACCID,
								CAST(@opbal AS FLOAT),
								@LEDGERID,
								0,
								CAST(@DATE AS DATE),
								'OPENING',
								'',
								'',
								@CMPID ,
								0 ,
								@USERID ,	    
								@YEARID ,	     	    
								0 ,
								getdate()	
							)
						
						END
					ELSE
						BEGIN
							
							select @ACCID = TEXTRADE.dbo.GET_LEDGER_ID('Opening A/C' ,@CMPID, 0,@YEARID)
							--***************** ACCID(CREDIT) AND ACCNAME(DEBIT) **********************
							INSERT INTO ACCMASTER
							VALUES
							(
								@LEDGERID,
								CAST(@opbal AS FLOAT),
								@ACCID,
								0,
								CAST(@DATE AS DATE),
								'OPENING',
								'',
								'',
								@CMPID ,
								0 ,
								@USERID ,	    
								@YEARID ,	     	    
								0 ,
								getdate()	
							)
							
						END
				END
				
					
			END
					
		fetch next from CUR_MAIN into @LEDGERNAME,@OPBAL, @DRCR

		end
close CUR_MAIN
deallocate CUR_MAIN
