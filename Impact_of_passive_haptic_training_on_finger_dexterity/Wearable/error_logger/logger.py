import logging
from os.path import dirname, abspath, join


dir_name = dirname(dirname(abspath(__file__)))
base_filename = 'waerable'
filename_suffix = 'log'
# os.path.join
full_path_file_name = join(dir_name, '.'.join(
    (base_filename, filename_suffix)))
logging.basicConfig(filename=full_path_file_name, level=logging.DEBUG,
                    format='%(asctime)s %(levelname)s %(name)s %(message)s')


def write_to_file(message):
    try:
        logging.debug(message)
    except:
        pass
