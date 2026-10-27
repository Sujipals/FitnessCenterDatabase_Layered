CREATE DATABASE IF NOT EXISTS FitnessCenter;

USE FitnessCenter;

CREATE TABLE IF NOT EXISTS MembershipTypes
(
    MembershipTypeID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    DurationMonths INT NOT NULL,
    CONSTRAINT chk_membership_price CHECK (Price >= 0),
    CONSTRAINT chk_membership_duration CHECK (DurationMonths > 0)
);

CREATE TABLE IF NOT EXISTS Trainers
(
    TrainerID INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    Specialization VARCHAR(100) NOT NULL
);

CREATE TABLE IF NOT EXISTS Members
(
    MemberID INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    Email VARCHAR(255) NOT NULL UNIQUE,
    MembershipTypeID INT NOT NULL,
    CONSTRAINT fk_members_membership
        FOREIGN KEY (MembershipTypeID)
        REFERENCES MembershipTypes(MembershipTypeID)
);

CREATE TABLE IF NOT EXISTS Classes
(
    ClassID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    StartTime DATETIME NOT NULL,
    DurationMinutes INT NOT NULL,
    Capacity INT NOT NULL,
    TrainerID INT NOT NULL,
    UNIQUE KEY UQ_Classes_Name_StartTime_Trainer
        (Name, StartTime, TrainerID),
    CONSTRAINT fk_classes_trainer
        FOREIGN KEY (TrainerID)
        REFERENCES Trainers(TrainerID),
    CONSTRAINT chk_class_duration
        CHECK (DurationMinutes > 0),
    CONSTRAINT chk_class_capacity
        CHECK (Capacity > 0)
);

CREATE TABLE IF NOT EXISTS Bookings
(
    BookingID INT AUTO_INCREMENT PRIMARY KEY,
    MemberID INT NOT NULL,
    ClassID INT NOT NULL,
    BookingDate DATETIME NOT NULL,
    UNIQUE KEY UQ_Bookings_Member_Class
        (MemberID, ClassID),
    CONSTRAINT fk_bookings_member
        FOREIGN KEY (MemberID)
        REFERENCES Members(MemberID),
    CONSTRAINT fk_bookings_class
        FOREIGN KEY (ClassID)
        REFERENCES Classes(ClassID)
);
CREATE TABLE IF NOT EXISTS BookingHistory
(
    BookingHistoryID INT AUTO_INCREMENT PRIMARY KEY,

    BookingID INT NOT NULL,

    MemberID INT NOT NULL,

    ClassID INT NOT NULL,

    Action VARCHAR(50) NOT NULL,

    ActionDate DATETIME NOT NULL,

    CONSTRAINT fk_history_booking
        FOREIGN KEY (BookingID)
        REFERENCES Bookings(BookingID),

    CONSTRAINT fk_history_member
        FOREIGN KEY (MemberID)
        REFERENCES Members(MemberID),

    CONSTRAINT fk_history_class
        FOREIGN KEY (ClassID)
        REFERENCES Classes(ClassID)
);
CREATE TABLE IF NOT EXISTS Payments
(
    PaymentID INT AUTO_INCREMENT PRIMARY KEY,
    MemberID INT NOT NULL,
    Amount DECIMAL(10,2) NOT NULL,
    PaymentDate DATE NOT NULL,
    PaymentMethod VARCHAR(50) NOT NULL
);
