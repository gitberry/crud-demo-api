use MahCoadz
select top 5 DateAdd(Hour, -6, DateTimeUTC) as LocalTime,  * from nbAPI1_log order by LocalTime desc
select MetaEdited as LocalTime, * from DataShort1 
where isactive = 1
and id > 2000
order by MetaEdited desc

/*
update datashort1 set isactive = 1 where ID  in (
--update datashort1 set isactive = 0 where ID not in (
1169
,132
--,90
,87
,124
,113
,99
,160)
*/