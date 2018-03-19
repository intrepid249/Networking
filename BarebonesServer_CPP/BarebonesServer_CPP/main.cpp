#include <iostream>
#include <string>
#include <WS2tcpip.h>

#pragma comment(lib, "ws2_32.lib")

int main()
{
	// Initialise winsock
	WSADATA wsData;
	WORD ver = MAKEWORD(2, 2);

	int wsOk = WSAStartup(ver, &wsData);
	if (wsOk != 0)
	{
		std::cerr << "Can't initialise winsock!";
		system("pause");
		return EXIT_FAILURE;
	}

	// Create a socket
	SOCKET listening = socket(AF_INET, SOCK_STREAM, 0);
	if (listening == INVALID_SOCKET)
	{
		std::cerr << "Can't create a socket!";
		WSACleanup();
		system("pause");
		return EXIT_FAILURE;
	}

	// Bind the ip address and port to a socket
	sockaddr_in hint;
	hint.sin_family = AF_INET;
	hint.sin_port = htons(65565);
	hint.sin_addr.S_un.S_addr = INADDR_ANY; // Could also use inet_pton...

	bind(listening, (sockaddr*)&hint, sizeof(hint));

	// Tell Winsock the socket is for listening
	listen(listening, SOMAXCONN); // SOMAXCONN is the maximum number of connections we can listen in on

	// Wait for a connection
	sockaddr_in client;
	int clientSize = sizeof(client);

	SOCKET clientSocket = accept(listening, (sockaddr*)&client, &clientSize);
	if (clientSocket == INVALID_SOCKET)
	{
		std::cerr << "Client socket is invalid";
		system("pause");
	}

	char host[NI_MAXHOST];		// Client's remote name
	char service[NI_MAXSERV];	// Service (i.e. port) the client is connected on

	ZeroMemory(host, NI_MAXHOST);	// Same as memset(host, 0, NI_MAXHOST);
	ZeroMemory(service, NI_MAXSERV);

	// Try to look up the host name of the client connecting
	if (getnameinfo((sockaddr*)&client, sizeof(client), host, NI_MAXHOST, service, NI_MAXSERV, 0) == 0)
	{
		std::cout << host << " connected on port " << service << std::endl;
	}
	else
	{
		inet_ntop(AF_INET, &client.sin_addr, host, NI_MAXHOST);
		std::cout << host << " connected on port " << ntohs(client.sin_port) << std::endl;
	}

	// Close listening socket
	closesocket(listening);

	// While loop: Accept and echo message back to client
	char buff[4096];

	while (true)
	{
		ZeroMemory(buff, 4096);
		// Wait for client to send data
		int bytesReceived = recv(clientSocket, buff, 4096, 0);
		if (bytesReceived == SOCKET_ERROR)
		{
			std::cerr << "Error in recv()" << std::endl;
			system("pause");
			break;
		}
		if (bytesReceived == 0)
		{
			std::cout << "Client disconnected" << std::endl;
			system("pause");
			break;
		}

		std::cout << std::string(buff, 0, bytesReceived) << std::endl;

		// Echo message back to client
		send(clientSocket, buff, bytesReceived + 1, 0);
	}

	// Close the socket
	closesocket(clientSocket);

	// Shutdown winsock
	WSACleanup();
}