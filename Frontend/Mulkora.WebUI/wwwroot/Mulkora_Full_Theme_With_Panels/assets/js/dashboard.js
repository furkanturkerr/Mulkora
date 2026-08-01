
(() => {
  const body = document.body;
  const toggle = document.querySelector('[data-sidebar-toggle]');
  const overlay = document.querySelector('.sidebar-overlay');
  const closeSidebar = () => body.classList.remove('sidebar-open');
  toggle?.addEventListener('click', () => body.classList.toggle('sidebar-open'));
  overlay?.addEventListener('click', closeSidebar);
  document.querySelectorAll('.workspace-nav a').forEach(link => link.addEventListener('click', () => {
    if (window.innerWidth <= 920) closeSidebar();
  }));

  document.querySelectorAll('[data-dropdown]').forEach(wrap => {
    const button = wrap.querySelector('[data-dropdown-button]');
    button?.addEventListener('click', event => {
      event.stopPropagation();
      document.querySelectorAll('[data-dropdown].is-open').forEach(open => {
        if (open !== wrap) open.classList.remove('is-open');
      });
      wrap.classList.toggle('is-open');
    });
  });
  document.addEventListener('click', () => document.querySelectorAll('[data-dropdown].is-open').forEach(x => x.classList.remove('is-open')));

  const toast = document.querySelector('.panel-toast');
  const showToast = (text) => {
    if (!toast) return;
    const target = toast.querySelector('[data-toast-text]');
    if (target) target.textContent = text;
    toast.classList.add('show');
    clearTimeout(window.__panelToastTimer);
    window.__panelToastTimer = setTimeout(() => toast.classList.remove('show'), 2600);
  };
  document.querySelectorAll('[data-toast]').forEach(btn => btn.addEventListener('click', event => {
    event.preventDefault();
    showToast(btn.dataset.toast || 'İşlem başarıyla tamamlandı.');
  }));

  document.querySelectorAll('[data-tabs]').forEach(group => {
    const buttons = group.querySelectorAll('[data-tab]');
    buttons.forEach(button => button.addEventListener('click', () => {
      buttons.forEach(x => x.classList.toggle('active', x === button));
      const root = group.closest('[data-tab-root]') || document;
      root.querySelectorAll('[data-tab-panel]').forEach(panel => panel.classList.toggle('active', panel.dataset.tabPanel === button.dataset.tab));
    }));
  });

  document.querySelectorAll('[data-select-all]').forEach(master => {
    master.addEventListener('change', () => {
      const table = master.closest('table');
      table?.querySelectorAll('tbody input[type="checkbox"]').forEach(box => box.checked = master.checked);
    });
  });

  document.querySelectorAll('[data-chat-input]').forEach(input => {
    const form = input.closest('form');
    form?.addEventListener('submit', event => {
      event.preventDefault();
      const value = input.value.trim();
      if (!value) return;
      const chat = document.querySelector('.chat-body');
      const message = document.createElement('div');
      message.className = 'message me';
      message.innerHTML = `${value.replace(/[<>]/g, '')}<time>Şimdi</time>`;
      chat?.appendChild(message);
      input.value = '';
      if (chat) chat.scrollTop = chat.scrollHeight;
    });
  });

  document.querySelectorAll('[data-filter-text]').forEach(input => {
    input.addEventListener('input', () => {
      const query = input.value.trim().toLocaleLowerCase('tr');
      document.querySelectorAll(input.dataset.filterText).forEach(row => {
        row.style.display = row.textContent.toLocaleLowerCase('tr').includes(query) ? '' : 'none';
      });
    });
  });
})();
