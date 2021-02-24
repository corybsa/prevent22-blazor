window.Prevent22 = {
	deferredPrompt: null,
	getHeight: () => window.innerHeight,
	getWidth: () => window.innerWidth,
	registerResizeCallback: () => {
		window.addEventListener("resize", Prevent22.resized);
	},
	resized: () => {
		DotNet.invokeMethodAsync("Prevent22.Client", "OnBrowserResize").then(data => data);
	},
	install: () => {
		Prevent22.hideInstallPrompt();
		Prevent22.deferredPrompt.prompt();

		Prevent22.deferredPrompt.userChoice.then(res => {
			if (res.outcome === 'accepted') {
				// user accepted
			} else {
				// user declined
			}

			Prevent22.deferredPrompt = null;
		})
	},
	showInstallPrompt: () => {
		$("#install-prompt").addClass("show");
	},
	hideInstallPrompt: () => {
		$("#install-prompt").removeClass("show");
	}
};

window.addEventListener("beforeinstallprompt", e => {
	e.preventDefault();
	Prevent22.deferredPrompt = e;
	Prevent22.showInstallPrompt();
});

window.addEventListener("appinstalled", () => {
	Prevent22.hideInstallPrompt();
	Prevent22.deferredPrompt = null;
});

function getPWADisplayMode() {
	const isStandalone = window.matchMedia('(display-mode: standalone)').matches;

	if (document.referrer.startsWith('android-app://')) {
		return 'twa';
	} else if (navigator.standalone || isStandalone) {
		return 'standalone';
	}
	return 'browser';
}

window.matchMedia('(display-mode: standalone)').addEventListener(e => {
	if (e.matches) {
		return 'standalone';
	}

	return 'browser';
});

navigator.serviceWorker.CACHE_VERSION = 1.1;