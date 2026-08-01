### read me file ###

requirments
-a local mysql server
- create a local instance with a user named="root" with a password="Iu7#01kp"
-run the following commands 
-- CREATE SCHEMA if not exists personal_assigment;
-- use personal_assigment;
-- CREATE TABLE if not exists course ( idcourse INT NOT NULL, course_title MEDIUMTEXT, instructor INT, PRIMARY KEY(idcourse));
-- CREATE TABLE if not exists instructors ( idinstructors INT NOT NULL, name MEDIUMTEXT, course_department VARCHAR(45), PRIMARY KEY(idinstructors)); 
-- CREATE TABLE if not exists student_grades ( id INT NOT NULL, idstudent INT, idcourse INT,grade FLOAT,PRIMARY KEY(id));
-- CREATE TABLE if not exists students ( idstudents INT NOT NULL, students_name MEDIUMTEXT, credits INT, PRIMARY KEY(idstudents));
instruction on how to run program
- in the folder there is a file called personal assigment.sln
- open this file with visual studio 2022
- then at top bar on visual studio there is a green button(next to debug drop down menu), make sure it set to https
- then press the button, and a website page should open up on default browser. note you may need to click the button twice,since first time may just build the project

Let me know if there is any troubles by contacting me at ldubell1@umbc.edu
