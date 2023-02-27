#define _CRT_SECURE_NO_WARNINGS

#include <iostream>
#include <WinSock2.h>


int serverInit();	// 소켓 초기화


// 소켓 정보를 담은 구조체7-
typedef struct sock_info {
	SOCKET s;
	HANDLE ev;
	char nick_name[50];
	char ip_addr[50];
}SOCK_INFO;



int iResult;
int port_number = 9999;					
constexpr const int client_count = 10;		// 연결 가능한 클라이언트 개수
SOCK_INFO sock_array[client_count + 1];		// 클라이언트 소켓정보를 담는 배열
int total_socket_count = 0;			// 현재 연결되어있는 소켓 개수 



int main() {


	return 0;
}



int serverInit() {
	WSADATA wsadata;			// Winsock
	SOCKET s;
	SOCKADDR_IN server_address;	//	ip 와 port 지정 구조체

	memset(&sock_array, 0, sizeof(sock_array));

	if (WSAStartup(MAKEWORD(2, 2), &wsadata) != 0) {

	}

}
