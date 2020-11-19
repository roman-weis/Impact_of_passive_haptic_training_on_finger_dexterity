def open_file_as_bytearray(full_path):
    trainings_piece = []
    try:
        with open(full_path, "r") as file:
            for line in file.readlines():
                chars = line.strip().split(' ')
                for vibration_motor_nr in chars:
                    if not vibration_motor_nr.isdigit():
                        continue
                    index = int(vibration_motor_nr)
                    arr = __index_to_bytearray(index)
                    trainings_piece.append(arr)
    except BaseException as e:
        print(e)
    finally:
        pass
    return trainings_piece


def __index_to_bytearray(index: int):
    if index == 1:
        return bytearray(b'\x01\x00\x00')
    elif index == 2:
        return bytearray(b'\x00\xfa\x00')
    elif index == 3:
        return bytearray(b'\x00\x01\x00')
    elif index == 4:
        return bytearray(b'\x00\x00\xfa')
    elif index == 5:
        return bytearray(b'\x00\x00\x01')
