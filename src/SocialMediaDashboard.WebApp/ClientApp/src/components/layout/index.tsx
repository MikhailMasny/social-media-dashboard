import * as React from "react";
import NavMenu from "../navMenu/";

export default (props: { children?: React.ReactNode }) => (
	<React.Fragment>
		<NavMenu />
		<div>
			{props.children}
		</div>
	</React.Fragment>
);
