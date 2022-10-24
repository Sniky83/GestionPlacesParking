USE GestionPlacesParking
GO

INSERT INTO ParkingSlot VALUES('P1')
INSERT INTO ParkingSlot VALUES('P2')
INSERT INTO ParkingSlot VALUES('P3')
INSERT INTO ParkingSlot VALUES('P4')

--PWD: apside_user
INSERT INTO [User] VALUES ('user@apside-groupe.com', '25707515faae7c87cf20d7be987c15f5533867ee003f49011d5d2913d3f5ad6b', 0)

--PWD: apside_administrateur
INSERT INTO [User] VALUES ('admin@apside-groupe.com', 'a5d2db9447f30b6f256a774b034a6c7eb2041824bb8bff84b717db637699e5e5', 1)
