USE FitnessCenter;

DROP PROCEDURE IF EXISTS GetMemberById;

DELIMITER //

CREATE PROCEDURE GetMemberById(IN p_MemberID INT)
BEGIN

    IF p_MemberID <= 0 THEN

        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Member ID must be greater than 0';

    ELSE

        SELECT
            MemberID,
            FirstName,
            LastName,
            Email,
            MembershipTypeID
        FROM Members
        WHERE MemberID = p_MemberID;

    END IF;

END //

DELIMITER ;