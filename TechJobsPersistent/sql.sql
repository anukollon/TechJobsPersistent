-- SHOW COLUMNS FROM techjobs.jobs;

-- SELECT COLUMN_NAME, DATA_TYPE FROM information_schema.columns
-- WHERE table_name = 'jobs';


delete from employers;
delete from jobs;
delete from jobskills;
delete from skills;
alter table employers auto_increment=1;
alter table jobs auto_increment=1;
alter table skills auto_increment=1;
commit;
