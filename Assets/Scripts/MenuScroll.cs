using SimpleFileBrowser;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class MenuScroll : MonoBehaviour
{
    [SerializeField] Menu m_menu;

    [SerializeField] GameObject Content;

    [SerializeField] Scrollbar scrollbar;

    string imagePath = "Assets/Resources/Images/";

    public List<GameObject> menuContents = new List<GameObject>(); 
    public List<GameObject> SpicyMenu = new List<GameObject>();
    public List<GameObject> CrispyMenu = new List<GameObject>();
    public List<GameObject> NotCravingMenu = new List<GameObject>();
    public List<GameObject> TooFarMenu = new List<GameObject>();
    public List<GameObject> WaitingMenu = new List<GameObject>();

    Coroutine colorOn;

    void Start()
    {
        for(int i = 0; i<m_menu.menu.Count; i++)
        {
            AddContent(i);
        }
        //시작시 이미지 불러와야함.
    }
    public void AddContent(int index)
    {
        GameObject temp = Instantiate(Content, transform);
        temp.name = index.ToString();
        Button button = temp.GetComponentInChildren<Button>();
        Image image = temp.GetComponentInChildren<Image>();
        if(File.Exists(imagePath + index +".png")) 
        {
            LoadImage(image, index);
        }
        button.onClick.AddListener(delegate { ImageBrowse(image,index); });
        Text[] texts = temp.GetComponentsInChildren<Text>();
        texts[0].text = m_menu.menu[index];//이름
        texts[1].text = m_menu.category[index].ToString();//카테고리
        texts[2].text = m_menu.info[index];//설명
        texts[3].text = m_menu.scope[index].ToString();//평점
        menuContents.Add(temp);
        AddMenu(m_menu.category[index], temp);
    }
    public void Remove(int index)
    {
        GameObject target = menuContents[index];
        menuContents.RemoveAt(index);
        RemoveMenu(m_menu.category[index], index);
        File.Delete(imagePath + index +".png");
        Destroy(target);
    }
    public void Search(int index)
    {
        scrollbar.value = (float)index / m_menu.menu.Count;
        GameObject obj = menuContents[index];
        Text[] texts= obj.GetComponentsInChildren<Text>();
        if (colorOn == null)
        {
            colorOn = StartCoroutine(ColorOn(texts));
        }
    }
    public void AddMenu(Category category, GameObject obj)
    {
        switch (category)
        {
            case Category.Spicy :
                SpicyMenu.Add(obj);
                break;
            case Category.Crispy:
                CrispyMenu.Add(obj);
                break;
            case Category.NotCraving:
                NotCravingMenu.Add(obj);
                break;
            case Category.TooFar:
                TooFarMenu.Add(obj);
                break;
            case Category.Waiting:
                WaitingMenu.Add(obj);
                break;
        }
    }
    public void RemoveMenu(Category category, int index)
    {
        switch (category)
        {
            case Category.Spicy:
                SpicyMenu.RemoveAt(index);
                break;
            case Category.Crispy:
                CrispyMenu.RemoveAt(index);
                break;
            case Category.NotCraving:
                NotCravingMenu.RemoveAt(index);
                break;
            case Category.TooFar:
                TooFarMenu.RemoveAt(index);
                break;
            case Category.Waiting:
                WaitingMenu.RemoveAt(index);
                break;
        }
    }
    public void ImageBrowse(Image image,int index)
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"), new FileBrowser.Filter("Text Files", ".txt", ".pdf"));

        FileBrowser.SetDefaultFilter(".jpg");

        FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");

        FileBrowser.AddQuickLink("Users", "C:\\Users", null);
        
        StartCoroutine(ShowLoadDialogCoroutine(image, index));
    }
    public void LoadImage(Image image, int index)//자동으로 로드될지 모르겠음.
    {
        byte[] bytes = File.ReadAllBytes(imagePath + index +".png");

        Texture2D texture = new Texture2D(1920, 1080);
        texture.LoadImage(bytes);

        Rect rect = new Rect(0, 0, 1920, 1080);
        image.sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
    }
    IEnumerator ShowLoadDialogCoroutine(Image image, int index)
    {
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load");

        Debug.Log(FileBrowser.Success);

        if (FileBrowser.Success)
        {
            for (int i = 0; i < FileBrowser.Result.Length; i++)
                Debug.Log(FileBrowser.Result[i]);

            byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);
            
            Texture2D texture = new Texture2D(1920, 1080);//이미지 사이즈가 안 맞음.
            texture.LoadImage(bytes);

            Rect rect = new Rect(0, 0, 1920, 1080);
            image.sprite = Sprite.Create(texture, rect, new Vector2(0.5f,0.5f));

            File.WriteAllBytes(imagePath + index + ".png", texture.EncodeToPNG());

            string destinationPath = Path.Combine(Application.persistentDataPath, FileBrowserHelpers.GetFilename(FileBrowser.Result[0]));
            FileBrowserHelpers.CopyFile(FileBrowser.Result[0], destinationPath);
        }
    }
    IEnumerator ColorOn(Text[] texts)
    {
        for(int i = 0; i< texts.Length; i++)
        {
            texts[i].color = Color.blue;
        }
        yield return new WaitForSeconds(5f);
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].color = Color.black;
        }
    }
}