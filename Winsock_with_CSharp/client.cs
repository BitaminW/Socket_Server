using System;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;
 
namespace Example
{
  class Program
  {
    // 실행 함수
    static void Main(string[] args)
    {
      // 업로드할 파일 정보를 취득한다.
      FileInfo file = new FileInfo("d:\\work\\nowonbuntistory.png");
      // stream을 취득한다.
      using (FileStream stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
      {
        // 파일 binary를 가져온다.
        byte[] data = new byte[file.Length];
        stream.Read(data, 0, data.Length);
        // 소켓을 연다.
        using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP))
        {
          // 파일 서버로 접속한다.
          socket.Connect(IPAddress.Parse("127.0.0.1"), 9090);
          // 전송 람다 함수 (C++과 약속한 규약대로 데이터 사이즈를 보내고 데이터를 보낸다.)
          Action<byte[]> Send = (b) =>
          {
            // 먼저 데이터 사이즈를 보내고.
            socket.Send(BitConverter.GetBytes(b.Length), 4, SocketFlags.None);
            // 데이터를 보낸다.
            socket.Send(b, b.Length, SocketFlags.None);
          };
          // 먼저 파일 명을 전송한다. (C++에서 \0를 보내지 않으면 메모리 구분이 되지 않으니 \0를 포함해서 보낸다.)
          // 이번에는 unicode가 아닌 utf8형식으로 전송합니다.
          Send(Encoding.UTF8.GetBytes("Download.png\0"));
          // 파일 바이너리 데이터를 보낸다.
          Send(data);
          // 서버로 부터 byte=1 데이터가 오면 클라이언트 종료한다.
          byte[] ret = new byte[1];
          socket.Receive(ret, 1, SocketFlags.None);
          if (ret[0] == 1)
          {
            Console.WriteLine("Completed");
          }
        }
      }
 
      Console.WriteLine("Press any key...");
      Console.ReadKey();
    }
  }
}
