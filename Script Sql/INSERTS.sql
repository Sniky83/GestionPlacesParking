USE GestionPlacesParking
GO

INSERT INTO ParkingSlot VALUES('P1')
INSERT INTO ParkingSlot VALUES('P2')
INSERT INTO ParkingSlot VALUES('P3')
INSERT INTO ParkingSlot VALUES('P4')

--PWD: apside_user
INSERT INTO [User] VALUES ('user@apside-groupe.com', '3fce61ebb4f305664ddd45110f3f4f27e32e61b052609e0a027b0f64f08e0801', 0)

--PWD: apside_administrateur
INSERT INTO [User] VALUES ('admin@apside-groupe.com', '7196165f31d788bc9e33eb64c47d88f5560c8d4a0806981eea986f13962ddf7f', 1)