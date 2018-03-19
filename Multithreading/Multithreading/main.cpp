#include <iostream>
#include <thread>
#include <vector>
#include <mutex>


//std::thread myThread(hello);
// This program will wait until myThread finishes
// it will then rejoin this thread and execution will continue
//myThread.join();

// If we want this to run on its own and spawn a Daemon
// it will run on its own and we won't need it to rejoin
// need to choose either join or detach
//myThread.detach();

void hello()
{
	static std::mutex coutMutex;

	coutMutex.lock();
	std::cout << "Hello\n";
	coutMutex.unlock();
}

int main()
{
	int maxThreads = 50;
	std::vector<std::thread> threads;

	for (size_t i = 0; i < maxThreads; i++)
	{
		threads.push_back(std::thread(hello));
	}
	

	system("pause");
}