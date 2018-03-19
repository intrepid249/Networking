#include <iostream>
#include <string>
#include <WS2tcpip.h>
#pragma comment(lib, "ws2_32.lib")

int main()
{
	std::string ipAddress;
	std::cout << "IP Address: ";
	std::cin >> ipAddress;

	int port;
	std::cout << "Port: ";
	std::cin >> port;

	// Initialise WinSock
	WSAData data;
	WORD ver = MAKEWORD(2, 2);
	int wsResult = WSAStartup(ver, &data);
	if (wsResult != 0)
	{
		std::cerr << "Can't start winsock, err #" << wsResult << std::endl;
		system("pause");
		return EXIT_FAILURE;
	}

	// Create Socket
	SOCKET sock = socket(AF_INET, SOCK_STREAM, 0);
	if (sock == INVALID_SOCKET)
	{
		std::cerr << "Can't create socket, Err #" << WSAGetLastError() << std::endl;
		WSACleanup();
		system("pause");
		return EXIT_FAILURE;
	}

	// Create hint structure
	sockaddr_in hint;
	hint.sin_family = AF_INET;
	hint.sin_port = htons(port);
	inet_pton(AF_INET, ipAddress.c_str(), &hint.sin_addr);

	// Connect to server
	int connResult = connect(sock, (sockaddr*)&hint, sizeof(hint));
	if (connResult == SOCKET_ERROR)
	{
		std::cerr << "Can't connect to server, err #" << WSAGetLastError() << std::endl;
		closesocket(sock);
		WSACleanup();
		system("pause");
		return EXIT_FAILURE;
	}

	// Do-While loop to send and receive data
	char buff[4096];
	std::string userInput;

	do
	{
		// Prompt the user for some text
		std::cout << ">>> ";
		std::getline(std::cin, userInput);

		if (userInput.size() > 0)
		{
			// Send the text (add 1 to the size since we have a trailing line termination character
			int sendResult = send(sock, userInput.c_str(), userInput.size() + 1, 0);
			if (sendResult != SOCKET_ERROR)
			{
				// Wait for response
				ZeroMemory(buff, 4096);
				int bytesReceived = recv(sock, buff, 4096, 0);
				if (bytesReceived > 0)
				{
					// Echo response to console
					std::cout << "SERVER>\t" << std::string(buff, 0, bytesReceived) << std::endl;
				}
			}
		}

	} while (userInput.size() > 0);

	// Gracefully close down everything
	closesocket(sock);
	WSACleanup();

	return EXIT_SUCCESS;
}