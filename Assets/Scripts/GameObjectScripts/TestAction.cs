using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAction : MonoBehaviour
{
	//自定义方向键的储存值
	public const int KEY_UP = 0;
	public const int KEY_DOWN = 1;
	public const int KEY_LEFT = 2;
	public const int KEY_RIGHT = 3;
	public const int KEY_FIRT = 4;

	//连续按键的事件限制
	public const int FRAME_COUNT = 100;
	//仓库中储存技能的数量
	public const int SAMPLE_SIZE = 3;
	//每组技能的按键数量
	public const int SAMPLE_COUNT = 5;
	//技能仓库
	int[,] Sample =
	{
	    //下 + 前 + 下 + 前 + 拳
		{KEY_DOWN,KEY_RIGHT,KEY_DOWN,KEY_RIGHT,KEY_FIRT},
		//下 + 前 + 下 + 后 + 拳
		{KEY_DOWN,KEY_RIGHT,KEY_DOWN,KEY_LEFT,KEY_FIRT},
		//下 + 后 + 下 + 后 + 拳
		{KEY_DOWN,KEY_LEFT,KEY_DOWN,KEY_LEFT,KEY_FIRT},
	};

	//记录当前按下按键的键值
	int currentkeyCode = 0;
	//标志是否开启监听按键
	bool startFrame = false;
	//记录当前开启监听到现在的时间
	int currentFrame = 0;
	//保存一段时间内玩家输入的按键组合
	List<int> playerSample;
	//标志完成操作
	bool isSuccess = false;

	// Start is called before the first frame update
	void Start()
    {
		playerSample = new List<int>();
	}

    // Update is called once per frame
    void Update()
    {
		UpdateKey();

		if (Input.anyKeyDown)
		{

			if (isSuccess)
			{
				//按键成功后重置
				isSuccess = false;
				Reset();
			}

			if (!startFrame)
			{
				//启动时间计数器
				startFrame = true;
			}

			//将按键值添加如链表中
			playerSample.Add(currentkeyCode);
			//遍历链表
			int size = playerSample.Count;
			if (size < SAMPLE_COUNT)//按键数不够
			{

			}
			else//如果玩家按下按键不小于设定按键，取后5位
			{
				for (int k = 0; k < size - SAMPLE_COUNT; k++)
				{
					playerSample.Remove(playerSample[k]);
				}
				for (int i = 0; i < SAMPLE_SIZE; i++)
				{
					int SuccessCount = 0;
					for (int j = 0; j < SAMPLE_COUNT; j++)
					{
						int temp = playerSample[j];
						if (temp == Sample[i, j])
						{
							SuccessCount++;
						}
					}
					//玩家按下的组合按键与仓库中的按键组合相同表示释放技能成功
					if (SuccessCount == SAMPLE_COUNT)
					{
						isSuccess = true;
						break;
					}
				}
			}

		}

		if (startFrame)
		{
			//计数器++
			currentFrame++;
		}

		if (currentFrame >= FRAME_COUNT)
		{
			//计数器超时
			if (!isSuccess)
			{
				Reset();
			}
		}
	}

	void Reset()
	{
		//重置按键相关信息
		currentFrame = 0;
		startFrame = false;
		playerSample.Clear();
	}

	void UpdateKey()
	{
		//获取当前键盘的按键信息
		if (Input.GetKeyDown(KeyCode.W))
		{
			currentkeyCode = KEY_UP;
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			currentkeyCode = KEY_DOWN;
		}
		if (Input.GetKeyDown(KeyCode.A))
		{
			currentkeyCode = KEY_LEFT;
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			currentkeyCode = KEY_RIGHT;
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			currentkeyCode = KEY_FIRT;
		}
	}
}
