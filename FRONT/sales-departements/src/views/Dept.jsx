import SideBar from "../templates/sidebars/SideBar";
import Header from "../templates/headers/Header";
import Request from "./Request";

export default function Dept() {
    return (
        <div class="wrapper d-flex align-items-stretch">
            <SideBar />
            <div className="main-panel">
                <Header />
                <div class="content">
                    <div class="container-fluid">
                        <div class="row">
                            <Request />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}