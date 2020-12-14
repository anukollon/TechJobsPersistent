
SELECT COLUMN_NAME, DATA_TYPE FROM information_schema.columns WHERE table_name = 'jobs';

SELECT NAME FROM EMPLOYERS WHERE LOCATION = 'St.Louis';

SELECT skills.Name as 'SKILL NAME',skills.Description as 'SKILL DESCRIPTION',
jobs.Name as 'JOB NAME' FROM skills INNER JOIN jobskills on 
skills.Id=jobskills.SkillId and jobskills.JobId is not null join jobs on jobs.Id=jobskills.JobId order by skills.Name ASC;


