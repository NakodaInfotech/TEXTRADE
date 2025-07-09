DECLARE @SUBJECT VARCHAR(100), @ENTRYBODY VARCHAR(MAX), @TOTALMTRS DECIMAL(18,2)
SELECT @TOTALMTRS = SUM(SALEORDER_DESC.SO_MTRS) FROM  SALEORDER INNER JOIN SALEORDER_DESC ON SALEORDER.SO_NO = SALEORDER_DESC.SO_NO AND SALEORDER.SO_YEARID = SALEORDER_DESC.SO_YEARID  WHERE CAST(SALEORDER_DESC.SO_CLOSEDDATE AS DATE) = CAST(GETDATE() AS DATE) AND SALEORDER_DESC.SO_RECDMTRS = 0 AND SALEORDER_DESC.SO_CLOSED = 'TRUE'
SET @SUBJECT = 'DAILY ORDER CANCELLATION REPORT FOR ' + CAST(DAY( GETDATE()) AS VARCHAR(2)) + '-' + CAST(MONTH( GETDATE()) AS VARCHAR(2)) + '-' + CAST(YEAR( GETDATE()) AS VARCHAR(4))

SET @ENTRYBODY = (SELECT  
[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@width]='250px', TD= LEDGERS.Acc_cmpname ,'', 
[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@align]='right',[TD/@width]='80px', TD=SUM(CAST(SALEORDER_DESC.SO_MTRS AS DECIMAL(18,2))) ,''
FROM  SALEORDER INNER JOIN SALEORDER_DESC ON SALEORDER.SO_NO = SALEORDER_DESC.SO_NO AND SALEORDER.SO_YEARID = SALEORDER_DESC.SO_YEARID INNER JOIN
                      LEDGERS ON SALEORDER.SO_LEDGERID = LEDGERS.Acc_id 
WHERE CAST(SALEORDER_DESC.SO_CLOSEDDATE AS DATE) = CAST(GETDATE() AS DATE) AND SALEORDER_DESC.SO_RECDMTRS = 0 AND SALEORDER_DESC.SO_CLOSED = 'TRUE'
GROUP BY LEDGERS.Acc_cmpname
FOR XML PATH ('tr'))


DECLARE @BODY VARCHAR(MAX)
SET @BODY = N'<H1 style="font-family:Tahoma; font-size:11px;">CANCELLED SUMMARY</H1>' +
				N'<Table Border = "1">' + 
				N'<Tr style="font-family:Tahoma; font-size:11px;"><Th>Name</Th><Th>Mtrs</Th></Tr>'+
				+ISNULL(@ENTRYBODY,'')+ N'<tfoot><tr><td colspan="1"; style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" bgcolor="yellow">GRAND TOTAL:</td>
				<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="yellow">' + CAST(@TOTALMTRS AS VARCHAR) + N'</td>
				</tr></tfoot></Table></html>'




--FOR DETAILS***********************************************************
begin
DECLARE @PREVIOUSNAME VARCHAR(100), @DETAILSBODY VARCHAR(MAX), @PARTYMTRS DECIMAL(18,2), @NAME VARCHAR(100), @SONO INT, @SODATE DATE, @DESIGNNO VARCHAR(100), @SHADE VARCHAR(100), @MTRS DECIMAL(18,2), @REASON VARCHAR(200)
SET @PREVIOUSNAME = ''
SET @PARTYMTRS = 0
SET @DETAILSBODY = ''
declare CUR_MAIN cursor for SELECT        ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, SALEORDER.so_no AS SONO, SALEORDER.so_date AS SODATE, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, 
                         '') AS SHADE, SUM(SALEORDER_DESC.SO_MTRS) AS MTRS, ISNULL(SALEORDER_DESC.SO_CLOSEDREASON,'') AS REASON
FROM            SALEORDER INNER JOIN
                         SALEORDER_DESC ON SALEORDER.so_no = SALEORDER_DESC.SO_NO AND SALEORDER.SO_YEARID = SALEORDER_DESC.SO_YEARID INNER JOIN
                         LEDGERS ON SALEORDER.so_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN
                         COLORMASTER ON SALEORDER_DESC.SO_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN
                         DESIGNMASTER ON SALEORDER_DESC.SO_DESIGNID = DESIGNMASTER.DESIGN_id
WHERE CAST(SALEORDER_DESC.SO_CLOSEDDATE AS DATE) = CAST(GETDATE() AS DATE) AND SALEORDER_DESC.SO_RECDMTRS = 0 AND SALEORDER_DESC.SO_CLOSED = 'TRUE'
GROUP BY ISNULL(LEDGERS.Acc_cmpname, ''), SALEORDER.so_no, SALEORDER.so_date, ISNULL(DESIGNMASTER.DESIGN_NO, ''), ISNULL(COLORMASTER.COLOR_name, ''), ISNULL(SALEORDER_DESC.SO_CLOSEDREASON,'')
ORDER BY NAME
open CUR_MAIN
	fetch next from CUR_MAIN into @NAME, @SONO, @SODATE, @DESIGNNO, @SHADE, @MTRS, @REASON
	while @@Fetch_STATUS = 0
		begin
			SET @DETAILSBODY = @DETAILSBODY + N'<tr>'
			IF @NAME <> @PREVIOUSNAME AND @PREVIOUSNAME <> ''
			BEGIN
				SET @PREVIOUSNAME = @NAME
				SET @DETAILSBODY = @DETAILSBODY + N'<TD colspan="5"; style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" bgcolor="yellow">TOTAL</TD>'
				SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="yellow">' + CAST(@PARTYMTRS as VARCHAR) + N'</TD>'
				set @PARTYMTRS = 0
				SET @DETAILSBODY = @DETAILSBODY + N'</tr>'
				SET @DETAILSBODY = @DETAILSBODY + N'<tr>'
			END
			SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" width="250px">' + @NAME + N'</TD>'
			SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" width="60px">' + CAST(@SONO as VARCHAR) + N'</TD>'
			SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" width="60px">' + CAST(@SODATE as VARCHAR) + N'</TD>'
			SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" width="150px">' + @DESIGNNO + N'</TD>'
			SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" width="100px">' + @SHADE + N'</TD>'
			SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" align="right" width="80px">' + CAST(@MTRS as VARCHAR) + N'</TD>'
			SET @DETAILSBODY = @DETAILSBODY + N'<TD style="font-family:Tahoma; font-size:11px;" width="250px">' + @REASON + N'</TD>'
			SET @PARTYMTRS = @PARTYMTRS + @MTRS
			SET @DETAILSBODY = @DETAILSBODY + N'</tr>'

			IF @PREVIOUSNAME = ''
			BEGIN 
				SET @PREVIOUSNAME = @NAME 
			END

			fetch next from CUR_MAIN into @NAME, @SONO, @SODATE, @DESIGNNO, @SHADE, @MTRS, @REASON
		END
close CUR_MAIN
deallocate CUR_MAIN
end

DECLARE @DBODY VARCHAR(MAX)
SET @DBODY = N'<H1 style="font-family:Tahoma; font-size:11px;">CANCELLED DETAILS</H1>' +
				N'<Table Border = "1">' + 
				N'<Tr style="font-family:Tahoma; font-size:11px;"><Th>Name</Th><Th>SO No</Th><Th>SO Dt</Th><Th>Design No</Th><Th>Shade</Th><Th>Mtrs</Th><Th>Reason</Th></Tr>'+
				+ISNULL(@DETAILSBODY,'')+ N'<tfoot><tr><td colspan="5"; style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" bgcolor="yellow">GRAND TOTAL:</td>
				<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="yellow">' + CAST(@TOTALMTRS AS VARCHAR) + N'</td>
				</tr></tfoot></Table></html>'

--FOR DETAILS***********************************************************


DECLARE @FINALBODY VARCHAR(MAX)
SET @FINALBODY = 'This is Just a Test Auto Mail To Show how the reports will look '  + @BODY + @DBODY


EXEC msdb.dbo.sp_send_dbmail
@profile_name='TEXTRADE',
@recipients='gulkitjain@gmail.com',	--infoavisindustries@gmail.com;gm@avisindustries.in;
@subject=@SUBJECT,
@body=@FINALBODY,
@body_format = 'HTML'