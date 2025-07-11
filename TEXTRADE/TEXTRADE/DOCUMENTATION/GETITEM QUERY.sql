SELECT ITEMMASTER.ITEM_NAME AS ITEMNAME, ISNULL(ITEMMASTER.ITEM_SELVEDGE,'') AS SELVEDGE, ISNULL(ITEMMASTER.item_remarks,'') AS REMARKS,
ISNULL(ITEM_WIDTH,'') AS WIDTH, HSN_CODE AS HSNCODE, ISNULL(CATEGORY_NAME,'') AS CATEGORY, ISNULL(ITEM_RATE,0) AS RATE

FROM ITEMMASTER INNER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID LEFT OUTER JOIN
CATEGORYMASTER ON item_categoryid = category_id
--WHERE HSN_CODE = '5407' AND  item_yearid = 3
ORDER BY ITEM_NAME 