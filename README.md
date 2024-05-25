Default.aspx
Default.aspx.cs dosyalarına bakabilirsiniz.

Db Tablo ve Store Procedure yapıları

CREATE TABLE [dbo].[Kullanici](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TcNo] [nvarchar](50) NULL,
	[Sifre] [nvarchar](64) NULL,
 CONSTRAINT [PK_Kullanici] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

1.SP

CREATE PROCEDURE [dbo].[usp_ValidateUser]
    @EncryptedTCNo NVARCHAR(256),
    @HashedSifre NVARCHAR(64)
AS
BEGIN
    SET NOCOUNT ON;
    -- Eşleşen bir kayıt olup olmadığını kontrol edin
    IF EXISTS (
        SELECT 1
        FROM Kullanici
        WHERE TCNo = @EncryptedTCNo AND Sifre = @HashedSifre
    )
    BEGIN
        SELECT 1 AS Result; -- Geçerli kullanıcı
    END
    ELSE
    BEGIN
        SELECT 0 AS Result; -- Geçersiz kullanıcı
    END
END

2.SP

CREATE PROCEDURE [dbo].[usp_KullaniciEkle]
    @TCNo nvarchar(50),
    @Sifre nvarchar(64)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @EncryptedTCNo nvarchar(256);
    DECLARE @HashedSifre nvarchar(64);
    -- Encrypt TCNo using SHA-256
    SET @EncryptedTCNo = CONVERT(nvarchar(256), HASHBYTES('SHA2_256', @TCNo), 2);
    -- Hash Sifre using SHA-256 (consider using a more secure approach with salt)
    SET @HashedSifre = CONVERT(nvarchar(64), HASHBYTES('SHA2_256', @Sifre), 2);
    INSERT INTO Kullanici (TCNo, Sifre) VALUES (@EncryptedTCNo, @HashedSifre);
END


