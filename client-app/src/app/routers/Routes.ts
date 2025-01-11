import {createBrowserRouter, RouteObject} from "react-router-dom";
import App from "../layout/App";
import ActivityForm from "../../features/form/ActivityForm";
import ActivityDashboard from "../../features/dashboard/ActivityDashboard";
import ActivityDetails from "../../features/details/ActivityDetails";
import TestErrors from "../../features/errors/TestError";
import NotFound from "../../features/errors/NotFound";
import ServerError from "../../features/errors/ServerError";
import LoginForm from "../../features/users/LoginForm";

export const routes: RouteObject[] = [
    {
        path: '/',
        Component: App,
        children: [
            {path: 'activities', Component: ActivityDashboard},
            {path: 'activities/:id', Component: ActivityDetails},
            {path: 'createActivity', Component: ActivityForm},
            {path: 'manage/:id', Component: ActivityForm},
            {path: 'login', Component: LoginForm},
            {path: 'errors', Component: TestErrors},
            {path: 'server-error', Component: ServerError},
            {path: 'not-found', Component: NotFound},
            {path: '*', Component: NotFound},
            
        ]
    }
]
export const router = createBrowserRouter(routes);