using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum moveType
{
    none = 0,
    pingpong,
    easeIn,
    easeOut,
    easeInOut,
}

public class testtree
{
    public testtree left;
    public testtree right;
    public int value;
    public int Level;

    public testtree(int value)
    {
        this.value = value;
    }
}


public class Test : MonoBehaviour {

    public void move(GameObject gameobject, Vector3 begin,Vector3 end,float time,bool pingpong)
    {
        IEnumerator enumerable = GoMove(gameobject, begin, end, time, pingpong);
        StartCoroutine(enumerable);
    }

    private IEnumerator GoMove(GameObject gameobject, Vector3 begin, Vector3 end, float time, bool pingpong)
    {
        Vector3 moveDic = end - begin;
        float step = (moveDic.magnitude / time / Time.deltaTime);
        int count = 0;
        while (count <= step)
        {
            count++;
            Vector3 newpos = new Vector3(Mathf.Lerp(begin.x, end.x, count/step), Mathf.Lerp(begin.y, end.y, count / step), Mathf.Lerp(begin.z, end.z, count / step));
            gameobject.transform.position = newpos;
            yield return 0;
        }
        yield return null;

        if (pingpong)
        {
            IEnumerator enumerable = GoMove(gameobject, end, begin, time, pingpong);
            StartCoroutine(enumerable);
        }
    }

    private void printTree(testtree root)
    {
        Queue<testtree> que = new Queue<testtree>();
        string str = "";
        root.Level = 0;
        que.Enqueue(root);
        int curLevel = 0;

        while (que.Count != 0)
        {
            testtree item = que.Dequeue();
            if(curLevel == item.Level )
            {
                curLevel++;
                str += item.value;
            }

            if (item.left!= null)
            {
                item.left.Level = item.Level + 1;
                que.Enqueue(item.left);
            }

            if(item.right != null)
            {
                item.right.Level = item.Level + 1;
                que.Enqueue(item.right);
            }
        }

    }

}


///框架UI结构
///UiManaget（UI管理）->UIGroup（UI组）->UiFormInfo（实例引用）->UIForm（实例）-> UIFormLogic(UI具体逻辑类)
///