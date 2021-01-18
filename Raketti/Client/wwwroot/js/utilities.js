window.raketti = {
	getHeight: () => window.innerHeight,
	getWidth: () => window.innerWidth,
	registerResizeCallback: () => {
		window.addEventListener("resize", raketti.resized);
	},
	resized: () => {
		DotNet.invokeMethodAsync("Raketti.Client", "OnBrowserResize").then(data => data);
	}
};