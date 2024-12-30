import {createBrowserRouter, RouteObject} from "react-router-dom";
import App from "../layout/App";
import ActivityForm from "../../features/form/ActivityForm";
import ActivityDashboard from "../../features/dashboard/ActivityDashboard";
import ActivityDetails from "../../features/details/ActivityDetails";


export const routes: RouteObject[] = [
    {
        path: '/',
        Component: App,
        children: [
            {path: 'activities', Component: ActivityDashboard},
            {path: 'activities/:id', Component: ActivityDetails},
            {path: 'createActivity', Component: ActivityForm},
            {path: 'manage/:id', Component: ActivityForm}
        ]
    }
]
export const router = createBrowserRouter(routes);