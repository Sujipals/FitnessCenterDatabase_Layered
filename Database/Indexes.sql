-- ============================================================
-- Fitness Center Database
-- Indexes
-- ============================================================

USE FitnessCenter;


-- ============================================================
-- INDEX 1
-- Primary Key
-- ============================================================
-- MemberID is the PRIMARY KEY of Members.
-- In MySQL/InnoDB, the primary key is the clustered
-- organization of the table data.
--
-- We already created this in CreateDatabase.sql:
--
-- MemberID INT AUTO_INCREMENT PRIMARY KEY
--
-- Therefore we do NOT create another index for MemberID.


-- ============================================================
-- INDEX 2
-- Secondary index
-- ============================================================
-- This index improves searches and sorting by LastName.

CREATE INDEX idx_members_lastname
ON Members(LastName);


-- ============================================================
-- SHOW INDEXES
-- ============================================================

SHOW INDEX FROM Members;