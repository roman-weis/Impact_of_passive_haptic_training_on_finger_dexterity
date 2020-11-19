import os
import asyncio
from passiv_training.training import Training
from file_manager.file_manager import open_file_as_bytearray
from notifications.notification import notify


duration_min = 25
file_name = ""
file_path = "/Users/romanweis/Desktop/Ba_Uebungen/Uebung_02/Exercise02.txt"


def start_training():
    piece_as_bytearray = open_file_as_bytearray(file_path)
    asyncio.run(Training().run(piece_as_bytearray=piece_as_bytearray,
                               duration_min=duration_min, start_bpm=100))


try:
    start_training()
except Exception as e:
    notify("Error", "", format(e))
else:
    notify("Training Completed!", "",
           f"""Passive training of {file_name} completed successfully.
           \n Duration: {duration_min} min""")
