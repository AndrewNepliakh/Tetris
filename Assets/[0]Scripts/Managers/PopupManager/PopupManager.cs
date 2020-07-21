using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "UIManager", menuName = "Managers/UIManager")]
public class PopupManager : BaseInjectable, IAwake
{
    private Transform _uiParent;
    private PoolManager _poolManager;

    private List<BasePopup> _allPopups = new List<BasePopup>();

    public void OnAwake()
    {
        try
        {
            _uiParent = GameObject.Find("UI_Canvas").GetComponent<Transform>();
        }
        catch(NullReferenceException e)
        {
            var eventSystemGo = new GameObject("EventSystem");
            eventSystemGo.AddComponent<EventSystem>();
            eventSystemGo.AddComponent<StandaloneInputModule>();
            
            var canvasGo = new GameObject("UI_Canvas");
            var canvas = canvasGo.AddComponent<Canvas>();
            var scaler = canvasGo.AddComponent<CanvasScaler>();
            canvasGo.AddComponent<GraphicRaycaster>();

            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1080.0f, 1920.0f);

            _uiParent = canvasGo.transform;
        }

        _poolManager = InjectBox.Get<PoolManager>();
    }
    
    public T ShowPopup<T>(object obj = null) where T : BasePopup
    {
        Pool pool = _poolManager.GetPool<T>();
        T popup = null;

        if (pool)
            if (pool.GetCount() > 0)
                popup = _poolManager.GetPool<T>()?.Activate<T>();

        if (popup)
        {
            popup.Show(obj);
            _allPopups.Add(popup);
            return popup;
        }
        else
        {
            var popupPrefab = Resources.Load<GameObject>("Prefabs/UI/" + typeof(T));
            var newPopup = _poolManager.GetOrCreate<T>(popupPrefab, _uiParent);
            newPopup.Show(obj);
            _allPopups.Add(newPopup);
            return newPopup;
        }
    }

    public void ClosePopup<T>(object obj = null) where T : BasePopup
    {
        var popup = GameObject.Find(typeof(T) + "(Clone)").GetComponent<T>();

        if (popup)
        {
            _poolManager.GetPool<T>()?.Deactivate(popup);
            popup.Close();
        }
    }

    public T GetPopup<T>() where T : BasePopup
    {
        return (T)_allPopups.Find(x => x.name == typeof(T).ToString());
    }
}