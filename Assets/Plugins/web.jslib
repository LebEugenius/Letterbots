mergeInto(LibraryManager.library, {

  SaveRecord: function (record) {
    kongregate.stats.submit("Record", record);
  },

});