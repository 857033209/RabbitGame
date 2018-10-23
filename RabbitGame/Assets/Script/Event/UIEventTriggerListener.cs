//ui事件触发类
/* Usage examples:
    EventTriggerListener.Get(gameObject).OnClick = print1;  -注册方法
    EventTriggerListener.Get(gameObject).OnUp += print2;  --添加方法
    EventTriggerListener.Get(gameObject).OnUp -= print1;  --删除方法

    void print1(GameObject go, PointerEventData ev)
    {
        Debug.Log(go.name);
    }
    void print2(GameObject go, PointerEventData ev)
    {
        Debug.Log(ev.position);
    }
*/
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public  class UIEventTriggerListener : EventTrigger
{

        public delegate void VoidDelegate(GameObject go);
        public delegate void EventDelegate(GameObject go, PointerEventData ev);
        [FormerlySerializedAs("onClick")]
        public VoidDelegate OnClick;
        [FormerlySerializedAs("onDown")]
        public EventDelegate OnDown;
        [FormerlySerializedAs("onExit")]
        public EventDelegate OnExit;
        [FormerlySerializedAs("onUp")]
        public EventDelegate OnUp;
        [FormerlySerializedAs("onSelect")]
        public VoidDelegate OnSelectEvent;
        [FormerlySerializedAs("onUpdateSelect")]
        public VoidDelegate OnUpdateSelect;
        [FormerlySerializedAs("onDragBegin")]
        public EventDelegate OnDragBegin;
        [FormerlySerializedAs("onDrag")]
        public EventDelegate OnDragEvent;
        [FormerlySerializedAs("onDragEnd")]
        public EventDelegate OnDragEnd;
        [FormerlySerializedAs("onEnter")]
        public EventDelegate OnEnter;
        [FormerlySerializedAs("onDrop")]
        public EventDelegate OnDropEvent;

        public delegate void ClickEffectDelegate();

        [FormerlySerializedAs("onClickEffect")]
        public ClickEffectDelegate OnClickEffect;

        private float _last_click_time;

        public bool ClickLimit = true;

        public static UIEventTriggerListener Get(GameObject go)
        {
            if (!go)
            {
                Debug.Log("UIEventTriggerListener.Get, GameObject is null!!");
            }
            UIEventTriggerListener listener = go.GetComponent<UIEventTriggerListener>();
            if (listener == null)
                listener = go.AddComponent<UIEventTriggerListener>();
            return listener;
        }

        // ReSharper disable once MemberCanBeMadeStatic.Local
        private bool _IsValidTrigger()
        {
            return true;
        }

        public override void OnPointerClick(PointerEventData event_data)
        {
            if (!this._IsValidTrigger())
            {
                return;
            }
            this._last_click_time = Time.unscaledTime;

            if (this.OnClickEffect != null)
            {
                this.OnClickEffect();
            }
            if (this.OnClick != null)
            {
                this.OnClick(this.gameObject);
            }
        }

        public override void OnPointerDown(PointerEventData event_data)
        {
            if (!this._IsValidTrigger())
            {
                return;
            }
            if (this.OnDown != null)
                this.OnDown(this.gameObject, event_data);
        }

        public override void OnPointerEnter(PointerEventData event_data)
        {
            if (!this._IsValidTrigger())
            {
                return;
            }
            if (this.OnEnter != null)
                this.OnEnter(this.gameObject, event_data);
        }

        public override void OnPointerExit(PointerEventData event_data)
        {
            if (!this._IsValidTrigger())
            {
                return;
            }
            if (this.OnExit != null)
                this.OnExit(this.gameObject, event_data);
        }

        public override void OnPointerUp(PointerEventData event_data)
        {
            if (!this._IsValidTrigger())
            {
                return;
            }

            if (this.OnUp != null)
                this.OnUp(this.gameObject, event_data);
        }

        public override void OnSelect(BaseEventData event_data)
        {
            if (!this._IsValidTrigger())
            {
                return;
            }
            if (this.OnSelectEvent != null)
                this.OnSelectEvent(this.gameObject);
        }

        public override void OnUpdateSelected(BaseEventData event_data)
        {
            if (!this._IsValidTrigger())
            {
                return;
            }
            if (this.OnUpdateSelect != null)
                this.OnUpdateSelect(this.gameObject);
        }

        public override void OnBeginDrag(PointerEventData event_data)
        {
            if (!this._IsValidTrigger())
            {
                return;
            }
            if (this.OnDragBegin != null)
                this.OnDragBegin(this.gameObject, event_data);
        }

        public override void OnDrag(PointerEventData event_data)
        {
            if (!this._IsValidTrigger())
            {
                return;
            }
            if (this.OnDragEvent != null)
                this.OnDragEvent(this.gameObject, event_data);
        }

        public override void OnEndDrag(PointerEventData event_data)
        {
            if (!this._IsValidTrigger())
            {
                return;
            }
            if (this.OnDragEnd != null)
                this.OnDragEnd(this.gameObject, event_data);
        }

        public override void OnDrop(PointerEventData event_data)
        {
            if (!this._IsValidTrigger())
            {
                return;
            }
            if (this.OnDropEvent != null)
                this.OnDropEvent(this.gameObject, event_data);
        }
    }
