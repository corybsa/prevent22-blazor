window.Prevent22 = {
	getHeight: () => window.innerHeight,
	getWidth: () => window.innerWidth,
	registerResizeCallback: () => {
		window.addEventListener("resize", Prevent22.resized);
	},
	resized: () => {
		DotNet.invokeMethodAsync("Prevent22.Client", "OnBrowserResize").then(data => data);
	}
};