pub struct MyDriver<SPI> {
    spi: SPI,
}

impl<SPI> MyDriver<SPI>
where
    SPI: SpiBus,
{
    pub fn new(spi: SPI) -> Self {
        Self { spi }
    }

    pub fn read_foo(&mut self) -> Result<[u8; 2], MyError<SPI::Error>> {
        let mut buf = [0; 2];
        self.spi.write(&[0x90]).map_err(MyError::Spi)?;
        self.spi.read(&mut buf).map_err(MyError::Spi)?;
        Ok(buf)
    }
}

#[derive(Copy, Clone, Debug)]
enum MyError<SPI> {
    Spi(SPI),
    // Add other errors for your driver here.
}
