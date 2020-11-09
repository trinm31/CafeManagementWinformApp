create database QuanLyQuanCafes
go 

use QuanLyQuanCafes
go


--Food 
--Table
--Food categories
--Account
--Bill
--BillInfor


drop TABLE TableFood
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Bàn chưa có tên',
	status NVARCHAR(100) NOT NULL DEFAULT N'Trống'	-- Trống || Có người
)
GO

CREATE TABLE Account
(
	UserName NVARCHAR(100) PRIMARY KEY,	
	DisplayName NVARCHAR(100) NOT NULL DEFAULT N'Kter',
	PassWord NVARCHAR(1000) NOT NULL DEFAULT 0,
	Type INT NOT NULL  DEFAULT 0 -- 1: admin && 0: staff
)
GO

CREATE TABLE FoodCategory
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên'
)
GO

Create TABLE Food
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
	idCategory INT NOT NULL,
	price FLOAT NOT NULL DEFAULT 0
	
	FOREIGN KEY (idCategory) REFERENCES dbo.FoodCategory(id)
	ON UPDATE CASCADE
    ON DELETE CASCADE
)
GO

Create TABLE Bill
(
	id INT IDENTITY PRIMARY KEY,
	DateCheckIn DATE NOT NULL DEFAULT GETDATE(),
	DateCheckOut DATE,
	idTable INT NOT NULL,
	status INT NOT NULL DEFAULT 0 -- 1: đã thanh toán && 0: chưa thanh toán
	
	FOREIGN KEY (idTable) REFERENCES dbo.TableFood(id)
	ON UPDATE CASCADE
    ON DELETE CASCADE
)
GO

create TABLE BillInfo
(
	id INT IDENTITY PRIMARY KEY,
	idBill INT NOT NULL,
	idFood INT NOT NULL,
	count INT NOT NULL DEFAULT 0
	
	FOREIGN KEY (idBill) REFERENCES dbo.Bill(id)
	ON UPDATE CASCADE
    ON DELETE CASCADE,
	FOREIGN KEY (idFood) REFERENCES dbo.Food(id)
	ON UPDATE CASCADE
    ON DELETE CASCADE
)
GO

INSERT INTO dbo.Account
        ( UserName ,
          DisplayName ,
          PassWord ,
          Type
        )
VALUES  ( N'K9' , -- UserName - nvarchar(100)
          N'RongK9' , -- DisplayName - nvarchar(100)
          N'1' , -- PassWord - nvarchar(1000)
          1  -- Type - int
        )
INSERT INTO dbo.Account
        ( UserName ,
          DisplayName ,
          PassWord ,
          Type
        )
VALUES  ( N'staff' , -- UserName - nvarchar(100)
          N'staff' , -- DisplayName - nvarchar(100)
          N'1' , -- PassWord - nvarchar(1000)
          0  -- Type - int
        )
GO

CREATE PROC USP_GetAccountByUserName
@UserName nvarchar(100)
AS
Begin 
	Select * From Account Where UserName = @UserName;
End
go


CREATE PROC USP_Login
@Username nvarchar(100), @PassWord nvarchar(100)
AS
Begin
	Select * From Account where UserName = @Username AND PassWord = @PassWord
End
Go

Declare @i int =0
While @i<=10
Begin 
	Insert TableFood(name) values(N'Ban' + CAST(@i as nvarchar(100)))
	set @i = @i+1
end
go


Create proc USP_GetTableList
As
begin
	select * from TableFood
end
go

update TableFood set status = N'co nguoi' where id = 276805

select * from TableFood


-- thêm bàn
DECLARE @i INT = 0

WHILE @i <= 10
BEGIN
	INSERT dbo.TableFood ( name)VALUES  ( N'Bàn ' + CAST(@i AS nvarchar(100)))
	SET @i = @i + 1
END
GO

CREATE PROC USP_GetTableList
AS SELECT * FROM dbo.TableFood
GO

UPDATE dbo.TableFood SET STATUS = N'Có người' WHERE id = 9

EXEC dbo.USP_GetTableList
GO

-- thêm category
INSERT dbo.FoodCategory
        ( name )
VALUES  ( N'Hải sản'  -- name - nvarchar(100)
          )
INSERT dbo.FoodCategory
        ( name )
VALUES  ( N'Nông sản' )
INSERT dbo.FoodCategory
        ( name )
VALUES  ( N'Lâm sản' )
INSERT dbo.FoodCategory
        ( name )
VALUES  ( N'Sản sản' )
INSERT dbo.FoodCategory
        ( name )
VALUES  ( N'Nước' )

-- thêm món ăn
INSERT dbo.Food
        ( name, idCategory, price )
VALUES  ( N'Mực một nắng nước sa tế', -- name - nvarchar(100)
          28, -- idCategory - int
          120000)
INSERT dbo.Food
        ( name, idCategory, price )
VALUES  ( N'Nghêu hấp xả', 29, 50000)
INSERT dbo.Food
        ( name, idCategory, price )
VALUES  ( N'Dú dê nướng sữa', 30, 60000)
INSERT dbo.Food
        ( name, idCategory, price )
VALUES  ( N'Heo rừng nướng muối ớt',31, 75000)
INSERT dbo.Food
        ( name, idCategory, price )
VALUES  ( N'Cơm chiên mushi', 29, 999999)
INSERT dbo.Food
        ( name, idCategory, price )
VALUES  ( N'7Up', 28, 15000)
INSERT dbo.Food
        ( name, idCategory, price )
VALUES  ( N'Cafe', 32, 12000)

-- thêm bill
INSERT	dbo.Bill
        ( DateCheckIn ,
          DateCheckOut ,
          idTable ,
          status
        )
VALUES  ( GETDATE() , -- DateCheckIn - date
          NULL , -- DateCheckOut - date
          276797 , -- idTable - int
          0  -- status - int
        )
        
INSERT	dbo.Bill
        ( DateCheckIn ,
          DateCheckOut ,
          idTable ,
          status
        )
VALUES  ( GETDATE() , -- DateCheckIn - date
          NULL , -- DateCheckOut - date
          276800, -- idTable - int
          0  -- status - int
        )
INSERT	dbo.Bill
        ( DateCheckIn ,
          DateCheckOut ,
          idTable ,
          status
        )
VALUES  ( GETDATE() , -- DateCheckIn - date
          GETDATE() , -- DateCheckOut - date
          276802 , -- idTable - int
          1  -- status - int
        )

-- thêm bill info
INSERT	dbo.BillInfo
        ( idBill, idFood, count )
VALUES  ( 43, -- idBill - int
          1, -- idFood - int
          2  -- count - int
          )
INSERT	dbo.BillInfo
        ( idBill, idFood, count )
VALUES  ( 38, -- idBill - int
          3, -- idFood - int
          4  -- count - int
          )
INSERT	dbo.BillInfo
        ( idBill, idFood, count )
VALUES  ( 40, -- idBill - int
          5, -- idFood - int
          1  -- count - int
          )
INSERT	dbo.BillInfo
        ( idBill, idFood, count )
VALUES  ( 46, -- idBill - int
          1, -- idFood - int
          2  -- count - int
          )
INSERT	dbo.BillInfo
        ( idBill, idFood, count )
VALUES  ( 50, -- idBill - int
          6, -- idFood - int
          2  -- count - int
          )
INSERT	dbo.BillInfo
        ( idBill, idFood, count )
VALUES  ( 51, -- idBill - int
          5, -- idFood - int
          2  -- count - int
          )         
          
GO


alter proc USP_InsertBill
@idTable int
as
begin
	INSERT	dbo.Bill
        ( DateCheckIn ,
          DateCheckOut ,
          idTable ,
          status,
		  Discount
		  
		  
        )
VALUES  ( GETDATE() , -- DateCheckIn - date
          null , -- DateCheckOut - date
          @idTable , -- idTable - int
          0,  -- status - int
		  0,

        )
end
go

alter proc USP_InsertBillInfo
@idBill int, @idFood int, @count int
as 
begin
	Declare @isExitsBillInfor int;
	Declare @foodCount int = 1
	select @isExitsBillInfor = id, @foodCount = count From BillInfo Where idBill = @idBill And idFood = @idFood
	if (@isExitsBillInfor > 0)
	begin 
		declare @newCount int = @foodCount + @count
		if (@newCount>0)
			update BillInfo set count = @foodCount + @count where idFood = @idFood
		else
			delete BillInfo where idBill = @idBill and idFood = @idFood
	end
	else 
	begin 
	INSERT	dbo.BillInfo
        ( idBill, idFood, count )
VALUES  ( @idBill, -- idBill - int
          @idFood, -- idFood - int
          @count  -- count - int
          )
	end
end
go
alter TRIGGER UTG_UpdateBillInfo
ON dbo.BillInfo FOR INSERT, UPDATE
AS
BEGIN
	DECLARE @idBill INT
	
	SELECT @idBill = idBill FROM Inserted
	
	DECLARE @idTable INT
	
	SELECT @idTable = idTable FROM dbo.Bill WHERE id = @idBill AND status = 0
	declare @count int
	select @count = COUNT(*) from BillInfo where idBill = @idBill
	if(@count > 0)
		UPDATE dbo.TableFood SET status = N'Có người' WHERE id = @idTable
	else
		UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable
END
GO

create TRIGGER UTG_UpdateBill
ON dbo.Bill FOR UPDATE
AS
BEGIN
	DECLARE @idBill INT
	
	SELECT @idBill = id FROM Inserted
	
	DECLARE @idTable INT
	
	SELECT @idTable = idTable FROM dbo.Bill WHERE id = @idBill
	
	DECLARE @count int = 0
	
	SELECT @count = COUNT(*) FROM dbo.Bill WHERE idTable = @idTable AND status = 0
	
	IF (@count = 0)
		UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable
END
GO

create proc usp_SwitchTable
@idTable1 int , @idTable2 int
as
begin
	declare @idFirstBill int
	declare @idSecondBill int

	declare @isFirstTableEmpty int = 1
	declare @isSecondTableEmpty int = 1

	Select @idSecondBill = id from bill where idTable = @idTable2 and status = 0
	Select @idFirstBill = id from bill where idTable = @idTable1 and status = 0

	print @idFirstBill
	print @idSecondBill
	print '--------------'

	if(@idFirstBill is null)
	begin
		print '000000000002'
		insert into Bill(dateCheckIn , dateCheckOut , idTable , status)
		values(
		GETDATE() ,
		null ,
		@idTable1 ,
		0
		)

		select @idFirstBill = max(id) from bill where idTable = @idTable1 and status = 0
		
	end

	select @isFirstTableEmpty = count(*) from dbo.BillInfo  where idBill = @idFirstBill

	print @idFirstBill
	print @idSecondBill
	print '--------------'

	if(@idSecondBill is null)
	begin  
		print '000000000001'
		insert into Bill(dateCheckIn , dateCheckOut , idTable , status)
		values(
		GETDATE() ,
		null ,
		@idTable2 ,
		0
		)

		select @idSecondBill = max(id) from bill where idTable = @idTable2 and status = 0

	end

	select @isSecondTableEmpty = count(*) from billInfo where idBill = @idSecondBill

	print @idFirstBill
	print @idSecondBill
	print '--------------'

	select id into IdBillInfoTable from dbo.billInfo where idBill = @idSecondBill

	update billInfo set idBill = @idSecondBill where idBill = @idFirstBill

	update billInfo set idBill = @idFirstBill where id in (select * from IdBillInfoTable)

	Drop table IdBillInfoTable

	if(@isFirstTableEmpty = 0)
		update tableFood set status = N'Trống' where id = @idTable2
	if(@isSecondTableEmpty = 0)
		update tableFood set status = N'Trống' where id = @idTable1
end
go

alter table Bill add Discount int

create proc USP_GetListBillByDate
@Checkin date, @CheckOUt date
as
begin
	select tableFood.name as [Tên bàn] , bill.totalPrice as [Tổng giá], Bill.dateCheckIn as [Ngày vào], Bill.dateCheckOut as [Ngày ra], Bill.Discount as [Giảm giá] 
	from Bill, tableFood
	where dateCheckIn >= @checkIn and dateCheckOut <= @checkOut and Bill.status = 1
	and tableFood.id = Bill.idTable 
end
go


alter proc USP_UpdateAccount
@Username nvarchar(100),
@DisplayName nvarchar(100),
@Password nvarchar(100),
@NewPassword nvarchar(100)
as
begin
	Declare @isRightPass int = 0;
	Select @isRightPass = COUNT(*) From Account Where UserName = @Username and PassWord = @Password
	If (@isRightPass = 1)
	begin
		if(@NewPassword = null or @NewPassword='')
		begin
			update Account set DisplayName = @DisplayName  Where UserName = @Username
		end
		else
		begin
			update Account set DisplayName = @DisplayName, PassWord = @NewPassword Where UserName = @Username
		end
	end
	

end
go

create trigger UTG_DeleteBillInfor on dbo.BillInfo for delete 
as
begin
	Declare @idBillInfo int
	Declare @idBill int
	Select @idBillInfo = id , @idBill = deleted.idBill From deleted

	Declare @idTable int =0
	Select @idTable = idTable From Bill where id = @idBill

	Declare @count int=0
	Select @count = COUNT(*) From BillInfo as bi, Bill as b where b.id = bi.idBill and b.id = @idBill and b.status =0;
	if(@count =0)
		update TableFood set status = N'Trống' where id = @idTable
end 
go





INSERT dbo.Food ( name, idCategory, price ) VALUES ( N'Mực một nắng nước sa tế', 1, 120000)

select * from FoodCategory

select * from Account

insert into  Account(UserName, DisplayName,PassWord,Type) values('tri' , 'TriNguyen' , 123 ,1)
update TableFood set status = N'Trống'


select * from TableFood
select * from Bill
select * from Food where idCategory = 2
select * from BillInfo
select * from FoodCategory

SELECT * FROM dbo.BillInfo WHERE idBill = 2;

update Bill set status = 0 where id =  2
update TableFood set status = N'Trống' where id =  276805

select *
from Bill where id =2

select * from BillInfo

delete Bill
delete BillInfo
SELECT f.name, bi.count, f.price, f.price*bi.count AS totalPrice FROM dbo.BillInfo AS bi, dbo.Bill AS b, dbo.Food AS f 
WHERE bi.idBill = b.id AND bi.idFood = f.id AND b.status = 0 AND b.idTable =276798

alter table bill
add Discount int

update bill set Discount = 0;

select * from TableFood
exec usp_SwitchTable  276796, 276797
select * from account where username = 'k9'

update Account set DisplayName = 'Tri Nguyen' where UserName = 'tri'
select * from Food where name = N'7Up'
update Food set name = N'' ,  idCategory = 1 , price =1 where id = 1

DELETE dbo.BillInfo WHERE idFood = 1
Delete Food where id = 1; 

select UserName,DisplayName, Type from Account

select * from Account

update Account set PassWord = N'20720532132149213101239102231223249249135100218' 

select * from FoodCategory
select * from Food
delete FoodCategory
delete Food
delete BillInfo
delete FoodCategory where id = 23;

update Food set idCategory = 0 where idCategory =23;
go
select * from TableFood where id
update TableFood set name = N'tri' where id = 276797
 