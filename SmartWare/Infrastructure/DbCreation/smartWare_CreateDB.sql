-- as root user
DROP USER IF EXISTS 'smartware'@'%';

DROP DATABASE IF EXISTS smartware;
CREATE DATABASE smartware;


-- let's go to the created database
USE smartware;

-- create a user that can connect from everywhere
-- REVOKE ALL PRIVILEGES ON mysql.proc FROM 'smartware'@'%';
-- REVOKE ALL PRIVILEGES ON *.* FROM 'smartware'@'%';
GRANT ALL ON smartware.* TO 'smartware'@'%' IDENTIFIED BY 'smartware';
GRANT SELECT ON mysql.proc TO 'smartware'@'%';
