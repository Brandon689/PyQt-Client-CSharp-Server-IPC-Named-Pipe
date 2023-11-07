import win32pipe, win32file
from PyQt6.QtWidgets import QApplication, QPushButton

def send_data():
    pipe_name = r'\\.\pipe\test_pipe'
    pipe = win32file.CreateFile(
        pipe_name,
        win32file.GENERIC_READ | win32file.GENERIC_WRITE,
        0,
        None,
        win32file.OPEN_EXISTING,
        0,
        None
    )
    
    # The message to send must be a byte string
    message = b'Hello, C# Backend!\n'
    win32file.WriteFile(pipe, message)
    
    # Close the pipe
    win32file.CloseHandle(pipe)
    print(f"Sent: {message.decode()}")

app = QApplication([])
window = QPushButton('Send Data to C# Backend')
window.clicked.connect(send_data)
window.show()
app.exec()
