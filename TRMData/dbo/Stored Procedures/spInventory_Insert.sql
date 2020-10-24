CREATE PROCEDURE [dbo].[spInventory_Insert]
	@ProductId int,
	@Quantity int,
	@PurchasePrice decimal,
	@PurchaseDate datetime2
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.Inventory(ProductId, Quantity, PurchasePrice, PurchaseDate)
	VALUES (@ProductId, @Quantity, @PurchasePrice, @PurchaseDate);
END