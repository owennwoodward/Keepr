    <template>
        <div class="modal fade" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-xl modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLongTitle">
                            <slot name="modal-title"></slot>
                        </h5>
                    </div>
                    <div class="modal-body">
                        <slot name="modal-body"></slot>
                    </div>
                </div>
            </div>
        </div>
    </template>