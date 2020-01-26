-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2020-01-26 12:53:31.242

-- tables
-- Table: Ingredient
CREATE TABLE Ingredient (
    name varchar(36)  NOT NULL,
    price int  NOT NULL,
    CONSTRAINT Ingredient_pk PRIMARY KEY  (name)
);

-- Table: Order
CREATE TABLE "Order" (
    uid varchar(36)  NOT NULL,
    pizzaDefinition text  NOT NULL,
    OrderStatus_name varchar(36)  NOT NULL,
    phone varchar(16)  NOT NULL,
    price int  NOT NULL,
    CONSTRAINT Order_pk PRIMARY KEY  (uid)
);

-- Table: OrderStatus
CREATE TABLE OrderStatus (
    name varchar(36)  NOT NULL,
    CONSTRAINT OrderStatus_pk PRIMARY KEY  (name)
);

-- Table: PizzaDefinition
CREATE TABLE PizzaDefinition (
    name varchar(36)  NOT NULL,
    CONSTRAINT PizzaDefinition_pk PRIMARY KEY  (name)
);

-- Table: PizzaIntegrients
CREATE TABLE PizzaIntegrients (
    id int  NOT NULL IDENTITY(1, 1),
    Pizza_name varchar(36)  NOT NULL,
    Ingredient_name varchar(36)  NOT NULL,
    CONSTRAINT PizzaIntegrients_pk PRIMARY KEY  (id)
);

-- Table: Promotion
CREATE TABLE Promotion (
    name varchar(36)  NOT NULL,
    minPrice int  NOT NULL,
    PromotionType_name varchar(36)  NOT NULL,
    CONSTRAINT name PRIMARY KEY  (name)
);

-- Table: PromotionType
CREATE TABLE PromotionType (
    name varchar(36)  NOT NULL,
    CONSTRAINT PromotionType_pk PRIMARY KEY  (name)
);

-- foreign keys
-- Reference: Order_OrderStatus (table: Order)
ALTER TABLE "Order" ADD CONSTRAINT Order_OrderStatus
    FOREIGN KEY (OrderStatus_name)
    REFERENCES OrderStatus (name);

-- Reference: PizzaIntegrients_Ingredient (table: PizzaIntegrients)
ALTER TABLE PizzaIntegrients ADD CONSTRAINT PizzaIntegrients_Ingredient
    FOREIGN KEY (Ingredient_name)
    REFERENCES Ingredient (name);

-- Reference: PizzaIntegrients_Pizza (table: PizzaIntegrients)
ALTER TABLE PizzaIntegrients ADD CONSTRAINT PizzaIntegrients_Pizza
    FOREIGN KEY (Pizza_name)
    REFERENCES PizzaDefinition (name);

-- Reference: Promotion_PromotionType (table: Promotion)
ALTER TABLE Promotion ADD CONSTRAINT Promotion_PromotionType
    FOREIGN KEY (PromotionType_name)
    REFERENCES PromotionType (name);

-- End of file.


-- Startup
INSERT INTO PromotionType VALUES('THIRD_PIZZA_GRATIS');
INSERT INTO PromotionType VALUES('THIRTY_PERCENT_OFF');
INSERT INTO PromotionType VALUES('FREE_DELIVERY');

INSERT INTO OrderStatus VALUES('WAITING_FOR_PAYMENT');
INSERT INTO OrderStatus VALUES('IN_REALIZATION');
INSERT INTO OrderStatus VALUES('WAITING_FOR_DELIVERY');
INSERT INTO OrderStatus VALUES('IN_DELIVERY');
INSERT INTO OrderStatus VALUES('DELIVERED');


