create or replace
PROCEDURE SP_GET_DEDL_XML 
IS
/***********************************************************************************************************
Procedure:      SP_GET_DEDL_XML
Purpose:        Retreives domain values (aka DEDL values) and updates an existing DEDL XML file 
Written by:     Doug Timms
Date created:   3/22/2011
Change log:     6/7/2011 final tweaks
***********************************************************************************************************/

-- *************************************************************************
--***************  VARIABLE DECLARATIONS   *********************************
-- *************************************************************************
  dedl_xml xmltype;
  namespace varchar2(250);
  l_tab   dbms_utility.uncl_array;
  l_tablen   number;
  stmnt varchar2(1000);
  l_cur sys_refcursor;
  fieldID varchar2(100);
  fieldName varchar2(100);
  iCount1 number;
  iCount2 number;
  
-- *************************************************************************
-- *****************  CURSOR DEFINITIONS   *********************************
-- *************************************************************************
-- * This cursor returns a series of XMLTypes or fragments   ***
-- * given a base XMLType and an XPath search string.        ***

  CURSOR crsrDEDL(dedl_doc XMLType, xpath VARCHAR2) IS
  SELECT value(p) XML FROM  TABLE(XMLSequence(extract(dedl_doc,xpath, namespace))) p;


-- *************************************************************************
-- ***************** PROCEDURE BEGINS **************************************
-- *************************************************************************
BEGIN
  dedl_xml := null;
  namespace := 'xmlns="http://www.exchangenetwork.net/schema/dedl/1"';
  
  delete from SYS_CONFIG where CONFIG_NAME = 'dedl_pub.config';
  commit;
  
  insert into SYS_CONFIG (CONFIG_ID, CONFIG_NAME, CONFIG_TYPE_CD, STATUS_CD, CONFIG_XML)
     (select 10005, 'dedl_pub.config', 'XML', 'A', CONFIG_XML from SYS_CONFIG where CONFIG_NAME = 'dedl.config');
  commit;  
  
  select CONFIG_XML into dedl_xml from SYS_CONFIG where CONFIG_NAME = 'dedl.config';

  FOR c IN crsrDEDL(dedl_xml,'//DataElement') LOOP
    
    if c.XML.extract('//DataSource/DataSourceType/text()',namespace) is not null then
      if c.XML.extract('//DataSource/DataSourceType/text()',namespace).getStringVal() = 'DBMS' then

        dbms_utility.comma_to_table(c.XML.extract('//DataSource/AccessStatement/text()',namespace).getStringVal(), l_tablen, l_tab);

        if l_tablen > 2 then

          --first need to check if the table exists at all
          select count(*) into iCount1 from all_tables where upper(rtrim(ltrim(table_name))) = upper(rtrim(ltrim((l_tab(3)))));
          select count(*) into iCount2 from all_views where upper(rtrim(ltrim(view_name))) = upper(rtrim(ltrim((l_tab(3)))));                    

--          dbms_output.put_line(upper(l_tab(3)) || ' ' || c.XML.extract('//DataSource/AccessStatement/text()',namespace).getStringVal() || ' ' || iCount1 || ' ' || iCount2);

          if iCount1+iCount2 > 0 then
            if l_tablen = 3 then --assume no where clause
               stmnt := 'select distinct ' || l_tab(1) || ' t, ' || l_tab(2) || ' from ' || l_tab(3) || ' order by t';
            elsif l_tablen = 5 then  --in this case a where clause is assumed to be included
               stmnt := 'select distinct ' || l_tab(1) || ' t, ' || l_tab(2) || ' from ' || l_tab(3) || ' where ' || l_tab(4) || ' = ''' || trim(l_tab(5)) || ''' order by t';
            end if;
                      
            --start loop once for each DEDL value
            open l_cur for stmnt;  
              loop
                fetch l_cur into fieldID, fieldName;
                exit when l_cur%notfound;
                  -- dbms_output.put_line(fieldID || ' ' || fieldName);
                
                  update sys_config t set t.config_xml = XMLType.appendChildXml(t.config_xml, 
                            '/DataElementList/DataElement[ElementIdentifier="' || c.XML.extract('//ElementIdentifier/text()',namespace).getStringVal() || '"]', 
                            XMLType('<ElementValue ValueLabel="' || dbms_xmlgen.convert(fieldName) ||'">' || dbms_xmlgen.convert(fieldID) ||'</ElementValue>'),
                            namespace) 
                  where CONFIG_NAME = 'dedl_pub.config';
                  commit;
              end loop;
            close l_cur;
            -- end DEDL value loop

          end if;   --end the if check of table or view existing                  
          
        end if;

      end if;

    end if;

    --now delete the DATA_SOURCE block since this is not needed
    update sys_config t set t.config_xml = XMLType.deleteXML(t.config_xml, 
              '/DataElementList/DataElement[ElementIdentifier="' || c.XML.extract('//ElementIdentifier/text()',namespace).getStringVal() || '"]/DataSource', 
              namespace) 
    where CONFIG_NAME = 'dedl_pub.config';
    commit;


  END LOOP;
    
  select CONFIG_XML into dedl_xml from SYS_CONFIG where CONFIG_NAME = 'dedl_pub.config';    
  update sys_config set config_clob = dedl_xml.getClobVal() where config_name = 'dedl_pub.config';

--EXCEPTION
--   WHEN OTHERS THEN  -- handles all other errors
--      dbms_output.put_line('Error occurred.');

END SP_GET_DEDL_XML;
/