import asyncio
from bleak import BleakClient, discover


async def scan():
    devices = await discover()
    result = list(
        map(lambda x: (x.name, x.address), devices)
    )
    return result


class ble(BleakClient):

    def __init__(self, device_address='B3ED868F-5CC4-43FC-B62B-42AE79C33C64'):
        super().__init__(device_address)

    async def __aenter__(self):
        await self.connect()
        return self

    async def __aexit__(self, exc_type, exc, tb):
        await self.disconnect()

    async def write_to_wearable(self, data_list: list, delay_in_sec=0):
        for service in self.services:
            for char in service.characteristics:
                for data in data_list:
                    if(type(data) == bytearray):
                        await self.write_gatt_char(char.uuid, data, response=False)
                        await asyncio.sleep(delay_in_sec)
                        await self.write_gatt_char(char.uuid, bytearray(b'\x00\x00\x00'), response=False)
                        await asyncio.sleep(delay_in_sec / 2)
