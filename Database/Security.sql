-- ============================================================
-- Fitness Center Database
-- Security and Least Privilege
-- ============================================================

USE FitnessCenter;


-- ============================================================
-- 1. Create normal application user
-- ============================================================

CREATE USER IF NOT EXISTS 'fitness_user'@'localhost'
IDENTIFIED BY 'ChangeThisPassword123!';


-- ============================================================
-- 2. Create administrator user
-- ============================================================

CREATE USER IF NOT EXISTS 'admin'@'localhost'
IDENTIFIED BY 'ChangeThisAdminPassword123!';


-- ============================================================
-- 3. Normal user permissions
-- ============================================================
-- The normal user can:
-- SELECT  = Read
-- INSERT  = Create
-- UPDATE  = Edit
--
-- DELETE is intentionally NOT granted.

GRANT SELECT, INSERT, UPDATE
ON FitnessCenter.*
TO 'fitness_user'@'localhost';


-- ============================================================
-- 4. Administrator permissions
-- ============================================================
-- Admin can also DELETE records.

GRANT SELECT, INSERT, UPDATE, DELETE
ON FitnessCenter.*
TO 'admin'@'localhost';


-- ============================================================
-- 5. Show permissions
-- ============================================================

SHOW GRANTS FOR 'fitness_user'@'localhost';

SHOW GRANTS FOR 'admin'@'localhost';