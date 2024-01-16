DROP TABLE IF EXISTS Articles;
DROP TABLE IF EXISTS Movements;


CREATE TABLE Articles (
  ArticleId BIGINT AUTO_INCREMENT PRIMARY KEY,
  ArticleType INT NOT NULL,
  Name TEXT NOT NULL,
  PartNumber TEXT NOT NULL,
  Note TEXT,
  Category TEXT,
  SubCategory1 TEXT,
  SubCategory2 TEXT,
  SubCategory3 TEXT,
  Room TEXT,
  Shelf TEXT,
  Box TEXT,
  Tag INT,
  Quantity INT DEFAULT 0,
  Cost DOUBLE NOT NULL,
  Price DOUBLE
);

CREATE TABLE Movements (
  MovementId BIGINT AUTO_INCREMENT PRIMARY KEY,
  ArticleId BIGINT,
  Date TEXT NOT NULL,
  MovementType INT NOT NULL,
  Reason INT NOT NULL,
  ProjectCode TEXT,
  UrlLink TEXT,
  Note TEXT,
  Quantity INT NOT NULL,
  Cost DOUBLE NOT NULL
);
ALTER TABLE Movements
ADD CONSTRAINT fk_article
FOREIGN KEY (ArticleId) REFERENCES Articles(ArticleId);