CREATE TABLE `usermgmt`.`user` ( `Id` INT NOT NULL AUTO_INCREMENT , `first_name` VARCHAR(50) NOT NULL , `last_name` VARCHAR(50) NOT NULL , `email` VARCHAR(50) NOT NULL , `phone` VARCHAR(50) NOT NULL , `comments` TEXT NOT NULL , `status` VARCHAR(10) NOT NULL DEFAULT 'active' , PRIMARY KEY (`Id`)) ENGINE = InnoDB;


INSERT INTO `user`
(`id`,`first_name`,`last_name`,`email`,`phone`,`comments`,`status`) VALUES
(NULL, 'Roselene','Cockwise','rcwise@ht.com','0115090001','None','inactive'),
(NULL, 'Atheenah','Jean-Joseph','ajeanjoseph@dvc.com','0115095594','None','active'),
(NULL, 'Robin','Jean Joseph','robin.jeanjoseph@ht.com','0115097888','None','active'),
(NULL, 'Kervens','Jean-Joseph','kjeanjoseph@ht.com','0115091210','None','active'),
(NULL, 'Denzel','Jean-Joseph','denzel.jeanjoseph@dvc.com','0115091210','None','active')

(NULL, 'Jovenele','Moise','jjmoise@ht.com','0115093151','None','active'),
(NULL, 'Vanell','Frederick','vfrederick@ht.com','0115094546','None','active'),
(NULL, 'Rachelle','Charles','ra-cc@ht.com','0115097888','None','active'),
(NULL, 'Elvila','Jean Joseph','lalabliss@ht.com','0115091210','None','active')