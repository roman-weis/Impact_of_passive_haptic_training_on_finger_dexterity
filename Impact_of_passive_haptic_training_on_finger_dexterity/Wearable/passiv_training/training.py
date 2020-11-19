import time
import asyncio
from bluetooth.ble import ble
from notifications.notification import notify


class Training():
    def __init__(self):
        self._running = True

    def terminate(self):
        self._running = False

    def __get_timeout_fom_minutes(self, minutes: float):
        assert 0.0 <= minutes <= 59.59
        return (time.time() + 60 * minutes)

    def __increase_bpm(self, bpm: int, max_bpm: int):
        return (bpm + 16)

    def __bpm_to_sec(self, bpm: int):
        return 60 / bpm

    async def run(self, piece_as_bytearray: bytearray, duration_min=30, start_bpm: int = 60, end_bpm: int = 400):
        assert end_bpm >= start_bpm
        timeout = self.__get_timeout_fom_minutes(duration_min)
        bpm = start_bpm
        async with ble() as ble_client:
            while time.time() < timeout and self._running:
                step_timeout = self.__get_timeout_fom_minutes(2)
                delay_in_sec = self.__bpm_to_sec(bpm)
                while time.time() < step_timeout and self._running:
                    await ble_client.write_to_wearable(piece_as_bytearray, delay_in_sec)

                bpm = self.__increase_bpm(bpm, end_bpm)
                await asyncio.sleep(3)
